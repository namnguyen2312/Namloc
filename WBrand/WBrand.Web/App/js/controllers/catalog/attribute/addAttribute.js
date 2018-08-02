//chart.js
angular
    .module('app')
    .controller('addCategoryCtrl', addCategoryCtrl)

addCategoryCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state'];
function addCategoryCtrl($scope, apiService, notificationService, $state) {

    $scope.data = {

    };

    $scope.save = save;

    function save() {
        $("input").prop('disabled', true);
        apiService.post('api/catalogAttribute/Add', $scope.data,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã được thêm mới.');
                $state.go('app.catalog.attribute');
            }, function (error) {
                $("input").prop('disabled', false);
                notificationService.displayError(error.data.Message);
            });
    }

}

