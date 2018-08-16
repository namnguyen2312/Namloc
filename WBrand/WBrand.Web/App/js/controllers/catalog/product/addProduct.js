//chart.js
angular
    .module('app')
    .controller('addProductCtrl', addProductCtrl)

addProductCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state'];
function addProductCtrl($scope, apiService, notificationService, $state) {

    $scope.data = {

    };

    $scope.save = save;
    $scope.toggleCategories = toggleCategories;

    function save() {
        $("input").prop('disabled', true);
        var product = {
            Product: $scope.data,
            CategoryIds: $scope.data.CategoryIds
        };
        apiService.post('api/product/Add', product,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã được thêm mới.');
                $state.go('app.catalog.product');
            }, function (error) {
                $("input").prop('disabled', false);
                notificationService.displayError(error.data.Message);
            });
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

