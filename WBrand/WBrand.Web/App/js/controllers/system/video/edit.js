//chart.js
angular
    .module('app')
    .controller('editVideoCtrl', editVideoCtrl)

editVideoCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams'];
function editVideoCtrl($scope, apiService, notificationService, $state, $stateParams) {

    $scope.data = {

    };

    $scope.save = save;

    function save() {
        $("input").prop('disabled', true);
        apiService.put('api/video/put', $scope.data,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã thêm mới.');
                $state.go('app.system.video');
            }, function (error) {
                $("input").prop('disabled', false);
                notificationService.displayError(error.data.Message);
            });
    }


    function loadDetail() {
        apiService.get('api/video/' + $stateParams.id, null, function (result) {
            $scope.data = result.data;
        }, function () {
            console.log('Cannot get data');
        });
    }

    loadDetail();
}