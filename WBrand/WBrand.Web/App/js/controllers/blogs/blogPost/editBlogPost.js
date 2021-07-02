//chart.js
angular
    .module('app')
    .controller('editBlogPostCtrl', editBlogPostCtrl)

editBlogPostCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams'];
function editBlogPostCtrl($scope, apiService, notificationService, $state, $stateParams) {

    $scope.data = {

    };

    $scope.save = save;
    $scope.chooseImage = chooseImage;

    function save() {
        $("input").prop('disabled', true);
        $scope.data.PublishDate = moment($scope.data.PublishDate, "DD/MM/YYYY HH:ss").format();
        apiService.put('api/blogPost/put', $scope.data,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã cập nhật.');
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

    function loadDetail() {
        apiService.get('api/blogPost/' + $stateParams.id, null, function (result) {
            $scope.data = result.data;
            $scope.data.PublishDate = moment(result.data.PublishDate).format('DD/MM/YYYY HH:ss');
        }, function () {
            console.log('Cannot get data');
        });
    }




    loadCategories();
    loadDetail();
    $('#publishDate').datetimepicker({
        format: 'd/m/Y H:i',
        lang: 'vi',
        closeOnDateSelect: true
    });

}