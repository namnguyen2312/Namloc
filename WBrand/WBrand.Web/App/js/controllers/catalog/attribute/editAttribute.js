//chart.js
angular
    .module('app')
    .controller('editAttributeCtrl', editAttributeCtrl)

editAttributeCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams'];
function editAttributeCtrl($scope, apiService, notificationService, $state, $stateParams) {

    $scope.data = {

    };

    $scope.save = save;

    function save() {
        $("input").prop('disabled', true);
        apiService.put('api/catalogAttribute/put', $scope.data,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã cập nhật.');
                $state.go('app.catalog.attribute');
            }, function (error) {
                $("input").prop('disabled', false);
                notificationService.displayError(error.data.Message);
            });
    }

    
    function loadDetail() {
        apiService.get('api/catalogAttribute/' + $stateParams.id, null, function (result) {
            $scope.data = result.data;
        }, function () {
            console.log('Cannot get data');
        });
    }

    loadDetail();
}

