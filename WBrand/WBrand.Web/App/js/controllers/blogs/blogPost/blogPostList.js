//chart.js
angular
    .module('app')
    .controller('blogPostCtrl', blogPostCtrl)

blogPostCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];
function blogPostCtrl($scope, apiService, notificationService, $ngBootbox) {

    $scope.pageSize = 20;
    $scope.pageIndex = 0;
    $scope.pagesCount = 0;
    $scope.totalCount = 0;
    $scope.categoryId = 0;
    $scope.data = [];

    $scope.search = search;
    $scope.deleteItem = deleteItem;

    function deleteItem(item) {
        apiService.del('api/blogPost/' + item.Id, null, function () {
            notificationService.displaySuccess('Xóa thành công');
            search();
        }, function () {
            notificationService.displayError('Xóa không thành công');
        });
    });
}

function loadCategory() {
    apiService.get('api/blogPostCategory/GetAllNoPaging', null, function (result) {
        var initCat = {
            Id: 0,
            Name: 'Chọn danh mục'
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
    apiService.get('/api/blogPost/getAll', config, function (result) {
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

