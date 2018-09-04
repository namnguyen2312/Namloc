//chart.js
angular
    .module('app')
    .controller('systemInfoCtrl', systemInfoCtrl)

systemInfoCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams'];
function systemInfoCtrl($scope, apiService, notificationService, $state, $stateParams) {

    $scope.data = {

    };

    $scope.save = save;

    function save() {
        $("button").prop('disabled', true);
        apiService.put('api/systemInfo/put', $scope.data,
            function (result) {
                notificationService.displaySuccess(' Đã cập nhật');
                $("button").prop('disabled', false);
            }, function (error) {
                $("input").prop('disabled', false);
                notificationService.displayError(error.data.Message);
            });
    }

    function loadDetail() {
        apiService.get('api/systemInfo/' , null, function (result) {
            $scope.data = result.data;
        }, function () {
            console.log('Cannot get data');
        });
    }

    loadDetail();
}