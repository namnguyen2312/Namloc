//chart.js
angular
    .module('app')
    .controller('addBlogPostCtrl', addBlogPostCtrl)

addBlogPostCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state'];
function addBlogPostCtrl($scope, apiService, notificationService, $state) {

    $scope.data = {
    };

    $scope.chooseImage = chooseImage;
    $scope.save = save;

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
            });
        };
        finder.popup();
    }


    function loadCategories() {
        apiService.get('api/catalogCategory/getAllNoPaging', null, function (result) {
            $scope.categories = result.data;
        }, function () {
            console.log('Cannot get data');
        });
    }


    loadCategories();
}

