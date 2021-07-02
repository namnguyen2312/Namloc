//chart.js
angular
    .module('app')
    .controller('addBlogCategoryCtrl', addBlogCategoryCtrl)

addBlogCategoryCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state'];
function addBlogCategoryCtrl($scope, apiService, notificationService, $state) {

    $scope.data = {
        
    };

    $scope.save = save;

    function save() {
        $("input").prop('disabled', true);
        apiService.post('api/blogPostCategory/Add', $scope.data,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã thêm mới.');
                $state.go('app.blog.category');
            }, function (error) {
                $("input").prop('disabled', false);
                notificationService.displayError(error.data.Message);
            });
    }

}

