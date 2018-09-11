//chart.js
angular
    .module('app')
    .controller('blogCategoryCtrl', blogCategoryCtrl)

blogCategoryCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];
function blogCategoryCtrl($scope, apiService, notificationService, $ngBootbox) {

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
            apiService.del('api/catalogCategory/delete', config, function () {
                notificationService.displaySuccess('Xóa thành công');
                search();
            }, function () {
                notificationService.displayError('Xóa không thành công');
            });
        });
    }

    function search(page) {
        page = page || 0;
        var config = {
            params: {
                filter: $scope.filter,
                page: page,
                pageSize: $scope.pageSize
            }
        };
        apiService.get('/api/blogPostCategory/getAll', config, function (result) {
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

    $scope.search();
}

