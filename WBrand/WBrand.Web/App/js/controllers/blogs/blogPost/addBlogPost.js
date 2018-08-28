//chart.js
angular
    .module('app')
    .controller('addBlogPostCtrl', addBlogPostCtrl)

addBlogPostCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state'];
function addBlogPostCtrl($scope, apiService, notificationService, $state) {

    $scope.data = {
    };
    $scope.moreImages = [];

    $scope.chooseImage = chooseImage;
    $scope.save = save;
    $scope.toggleCategories = toggleCategories;

    function save() {
        $("input").prop('disabled', true);

        apiService.post('api/blogPost/Add', $scope.data,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã được thêm mới.');
                $state.go('app.blog.post');
            }, function (error) {
                $("input").prop('disabled', false);
                notificationService.displayError(error.data.Message);
            });
    }

    function chooseImage(item) {
        var finder = new CKFinder();
        finder.selectActionFunction = function (fileUrl) {
            $scope.$apply(function () {
                $scope.data.Image = fileUrl;
            })
        };
        finder.popup();
    };

    function chooseMoreImage(opt) {
        var finder = new CKFinder();


        finder.selectActionFunction = function (fileUrl, dataFile, files) {
            $scope.$apply(function () {
                switch (opt) {
                    case 1:
                        for (var i = 0; i < files.length; i++) {
                            $scope.moreImages.push(files[i].url);
                        }
                        break;
                    default:
                        for (var i = 0; i < files.length; i++) {
                            $scope.moreImagesTechnical.push(files[i].url);
                        }
                }

            })
        };



        finder.popup();
    }

    function deleteMoreImage(opt) {

        switch (opt) {
            case 1:
                if ($scope.moreImages.length != 0) {
                    $scope.moreImages.splice($scope.moreImages.length - 1, 1);
                }
            default:
                if ($scope.moreImagesTechnical.length != 0) {
                    $scope.moreImagesTechnical.splice($scope.moreImagesTechnical.length - 1, 1);
                }
        }

    }

    function loadCategories() {
        apiService.get('api/catalogCategory/getAll', null, function (result) {
            $scope.categories = result.data;
        }, function () {
            console.log('Cannot get data');
        });
    }

    function toggleCategories(categoryId) {
        var index = $scope.data.CategoryIds.indexOf(categoryId);
        if (index > -1) {
            $scope.data.CategoryIds.splice(index, 1);
            var childCategoryIds = getChildCategoryIds(categoryId);
            childCategoryIds.forEach(function spliceChildCategory(childCategoryId) {
                index = $scope.data.CategoryIds.indexOf(childCategoryId);
                if (index > -1) {
                    $scope.data.CategoryIds.splice(index, 1);
                }
            });
        } else {
            $scope.data.CategoryIds.push(categoryId);
            var category = $scope.categories.find(function (item) { return item.Id === categoryId; });
            if (category) {
                var parentCategoryIds = getParentCategoryIds(category.ParentId);
                parentCategoryIds.forEach(function pushParentCategory(parentCategoryId) {
                    if ($scope.data.CategoryIds.indexOf(parentCategoryId) < 0) {
                        $scope.data.CategoryIds.push(parentCategoryId);
                    }
                });
            }
        }
    };

    function getParentCategoryIds(categoryId) {
        if (!categoryId) {
            return [];
        }
        var category = $scope.categories.find(function (item) { return item.Id === categoryId; });

        return category ? [category.Id].concat(getParentCategoryIds(category.ParentId)) : [];
    }

    function getChildCategoryIds(categoryId) {
        if (!categoryId) {
            return [];
        }
        var result = [];
        var queue = [];
        queue.push(categoryId);
        while (queue.length > 0) {
            var current = queue.shift();
            result.push(current);
            var childCategories = $scope.categories.filter(function (item) { return item.ParentId === current; });
            childCategories.forEach(function pushChildCategoryToTheQueue(childCategory) {
                queue.push(childCategory.Id);
            });
        }

        return result;
    }

    loadCategories();
}

