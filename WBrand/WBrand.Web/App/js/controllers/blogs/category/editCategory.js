//chart.js
angular
    .module('app')
    .controller('editBlogCategoryCtrl', editBlogCategoryCtrl)

editBlogCategoryCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state','$stateParams'];
function editBlogCategoryCtrl($scope, apiService, notificationService, $state, $stateParams) {

    $scope.data = {

    };

    $scope.save = save;

    function save() {
        $("input").prop('disabled', true);
        apiService.put('api/blogPostCategory/put', $scope.data,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã cập nhật.');
                $state.go('app.blog.category');
            }, function (error) {
                $("input").prop('disabled', false);
                notificationService.displayError(error.data.Message);
            });
    }

    function loadDetail() {
        apiService.get('api/blogPostCategory/' + $stateParams.id, null, function (result) {
            $scope.data = result.data;
        }, function () {
            console.log('Cannot get data');
        });
    }


    loadDetail();
}

