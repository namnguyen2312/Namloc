//chart.js
angular
    .module('app')
    .controller('bannerCtrl', bannerCtrl)

bannerCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];
function bannerCtrl($scope, apiService, notificationService, $ngBootbox) {

    $scope.data = [];
    $scope.search = search;
    $scope.del = del;

    function del(item) {
        $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
            apiService.del('api/banner/' + item.Id, null, function () {
                notificationService.displaySuccess('Xóa thành công');
                search();
            }, function () {
                notificationService.displayError('Xóa không thành công');
            });
        });
    }

    function search() {
        var config = {
            params: {
                position: $scope.position
            }
        };
        apiService.get('/api/banner/getAll', config, function (result) {
            $scope.data = result.data;
        }, function () {
            console.log('load failed data');
        });
    }

    search();
}