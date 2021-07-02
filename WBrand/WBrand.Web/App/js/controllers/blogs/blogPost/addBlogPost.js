//chart.js
angular
    .module('app')
    .controller('addBlogPostCtrl', addBlogPostCtrl)

addBlogPostCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state'];
function addBlogPostCtrl($scope, apiService, notificationService, $state) {
    var d = new Date();
    var month = d.getMonth();
    var day = d.getDate();
    var year = d.getFullYear();
    $scope.data = {
        CategoryId: 0
    };

    $scope.chooseImage = chooseImage;
    $scope.save = save;

    function save() {
        $("input").prop('disabled', true);
        $scope.data.PublishDate = $('#publishDate').datetimepicker('getValue');
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
            });
        };
        finder.popup();
    }


    function loadCategories() {
        apiService.get('api/blogPostCategory/getAllNoPaging', null, function (result) {
            var initCat = {
                Id: 0,
                Name: 'Chọn danh mục'
            };
            $scope.categories = result.data;
            $scope.categories.unshift(initCat);
        }, function () {
            console.log('Cannot get data');
        });
    }

    $('#publishDate').datetimepicker({
        format: 'd/m/Y H:i',
        lang: 'vi',
        defaultDate: new Date(),
        closeOnDateSelect: true
    });
    loadCategories();
}

