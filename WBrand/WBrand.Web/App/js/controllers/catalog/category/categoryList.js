//chart.js
angular
    .module('app')
    .controller('categoryCtrl', categoryCtrl)

categoryCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];
function categoryCtrl($scope, apiService, notificationService, $ngBootbox) {

    $scope.data = [];
    $scope.search = search;
    $scope.del = del;

    function del(item) {
        $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
            apiService.del('api/catalogCategory/' + item.Id, null, function () {
                notificationService.displaySuccess('Xóa thành công');
                search();
            }, function () {
                notificationService.displayError('Xóa không thành công');
            });
        });
    }

    function search() {
        apiService.get('/api/catalogCategory/getAll', null, function (result) {
            $scope.data = result.data;
        }, function () {
            console.log('load failed data');
        });
    }

    $scope.search();
}

