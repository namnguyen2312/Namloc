//chart.js
angular
    .module('app')
    .controller('editBlogPostCtrl', editBlogPostCtrl)

editBlogPostCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams'];
function editBlogPostCtrl($scope, apiService, notificationService, $state, $stateParams) {

    $scope.data = {

    };

    $scope.save = save;

    function save() {
        $("input").prop('disabled', true);
        apiService.put('api/blogPost/put', $scope.data,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã cập nhật.');
                $state.go('app.blog.category');
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

    function loadDetail() {
        apiService.get('api/blogPost/' + $stateParams.id, null, function (result) {
            $scope.data = result.data;
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
    loadDetail();
}