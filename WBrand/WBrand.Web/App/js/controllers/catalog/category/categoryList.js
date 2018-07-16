//chart.js
angular
    .module('app')
    .controller('categoryCtrl', categoryCtrl)

categoryCtrl.$inject = ['$scope', 'apiService', 'notificationService'];
function categoryCtrl($scope, apiService, notificationService) {

    $scope.data = [];
    $scope.search = search;

    function search() {
        apiService.get('/api/catalogCategory/getAll', null, function (result) {
            $scope.data = result.data;
        }, function () {
            console.log('load failed data');
        });
    }

    $scope.search();
}

