//chart.js
angular
    .module('app')
    .controller('attributeCtrl', attributeCtrl)

attributeCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];
function attributeCtrl($scope, apiService, notificationService, $ngBootbox) {

    $scope.data = [];
    $scope.search = search;
    $scope.del = del;

    function del(item) {
        $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
            var config = {
                params: {
                    id: item.Id
                }
            }
            apiService.del('api/catalogAttribute/delete', config, function () {
                notificationService.displaySuccess('Xóa thành công');
                search();
            }, function () {
                notificationService.displayError('Xóa không thành công');
            })
        });
    }

    function search() {
        apiService.get('/api/catalogAttribute/getAll', null, function (result) {
            $scope.data = result.data;
        }, function () {
            console.log('load failed data');
        });
    }

    $scope.search();
}

