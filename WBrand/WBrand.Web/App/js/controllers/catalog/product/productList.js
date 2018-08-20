//chart.js
angular
    .module('app')
    .controller('productCtrl', productCtrl)

productCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];
function productCtrl($scope, apiService, notificationService, $ngBootbox) {

    $scope.pageSize = 20;
    $scope.pageIndex = 0;
    $scope.pagesCount = 0;
    $scope.totalCount = 0;
    $scope.data = [];
    $scope.search = search;
    $scope.del = del;

    function del(item) {
        $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
            var config = {
                params: {
                    id: item.Id
                }
            };
            apiService.del('api/product/delete', config, function () {
                notificationService.displaySuccess('Xóa thành công');
                search();
            }, function () {
                notificationService.displayError('Xóa không thành công');
            })
        });
    }

    function loadCategory() {
        apiService.get('api/catalogCategory/getAll', null, function (result) {
            var initCat = {
                Id: 0,
                Name: 'Chọn'
            };
            $scope.categories = result.data;
            $scope.categories.unshift(initCat);
        }, function () {
            console.log('Cannot get data');
        });
    }


    function search(page) {
        page = page || 0;
        var config = {
            params: {
                filter: $scope.filter,
                categoryId: $scope.categoryId,
                page: page,
                pageSize: $scope.pageSize
            }
        };
        apiService.get('/api/product/getAll', config, function (result) {
            if (result.data.TotalCount == 0) {
                notificationService.displayWarning('Không có bản ghi nào được tìm thấy.');
            }
            $scope.data = result.data.Items;
            $scope.page = result.data.Page;
            $scope.pagesCount = result.data.TotalPages;
            $scope.totalCount = result.data.TotalCount;
            $scope.loading = false;
        }, function () {
            console.log('load failed data');
            $scope.loading = false;
        });
    }

    search();
    loadCategory();
}

