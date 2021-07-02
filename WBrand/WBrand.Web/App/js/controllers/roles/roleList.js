﻿//chart.js
angular
    .module('app')
    .controller('roleCtrl', roleCtrl)

roleCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox'];
function roleCtrl($scope, apiService, notificationService, $ngBootbox) {

    $scope.loading = true;
    $scope.keyword = "";
    $scope.data = [];
    $scope.page = 0;
    $scope.pageCount = 0;
    $scope.search = search;
    $scope.clearSearch = clearSearch;

    function search(page) {
        page = page || 0;

        $scope.loading = true;
        var config = {
            params: {
                page: page,
                pageSize: 10,
                filter: $scope.keyword
            }
        };

        apiService.get('api/appRole/allPage', config, dataLoadCompleted, dataLoadFailed);
    }

    function dataLoadCompleted(result) {
        $scope.data = result.data.Items;
        $scope.page = result.data.Page;
        $scope.pagesCount = result.data.TotalPages;
        $scope.totalCount = result.data.TotalCount;
        $scope.loading = false;

        if ($scope.filterExpression && $scope.filterExpression.length) {
            notificationService.displayInfo(result.data.Items.length + ' được tìm thấy');
        }
    }
    function dataLoadFailed(response) {
        notificationService.displayError(response.data);
    }

    function clearSearch() {
        $scope.filterExpression = '';
        search();
    }

    $scope.search();
}

