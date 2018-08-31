//chart.js
angular
    .module('app')
    .controller('slideShowCtrl', slideShowCtrl)

slideShowCtrl.$inject = ['$scope', 'apiService', 'notificationService','$ngBootbox'];
function slideShowCtrl($scope, apiService, notificationService, $ngBootbox) {

    $scope.data = [];
    $scope.position = 0;
    $scope.search = search;
    $scope.del = del;

    function del(item) {
        $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
            var config = {
                params: {
                    id: item.Id
                }
            };
            apiService.del('api/slideShow/delete', config, function () {
                notificationService.displaySuccess('Xóa thành công');
                search();
            }, function () {
                notificationService.displayError('Xóa không thành công');
            });
        });
    }

    function search() {
        var config ={
            position: $scope.position
        }
        apiService.get('/api/slideShow/getAll', config, function (result) {
            $scope.data = result.data;
        }, function () {
            console.log('load failed data');
        });
    }

    $scope.search();
}