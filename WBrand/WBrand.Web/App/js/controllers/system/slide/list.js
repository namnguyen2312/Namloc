//chart.js
angular
    .module('app')
    .controller('slideShowCtrl', slideShowCtrl)

slideShowCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];
function slideShowCtrl($scope, apiService, notificationService, $ngBootbox) {

    $scope.data = [];
    $scope.position = 0;
    $scope.search = search;
    $scope.del = del;

    function del(item) {
        $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
            apiService.del('api/slideShow/' + item.Id, null, function () {
                notificationService.displaySuccess('Xóa thành công');
                search();
            }, function () {
                notificationService.displayError('Xóa không thành công');
            });
        });
    }

    function loadPosition() {
        apiService.get('api/slideShow/getAllPosition', null, function (result) {
            $scope.positions = result.data;
        }, function () {
            console.log('Cannot get data');
        });
    }

    function search() {
        var config = {
            params: {
                position: $scope.position
            }
        };
        apiService.get('/api/slideShow/getAll', config, function (result) {
            $scope.data = result.data;
        }, function () {
            console.log('load failed data');
        });
    }

    search();
    loadPosition();
}