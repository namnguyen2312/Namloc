//chart.js
angular
    .module('app')
    .controller('editCategoryCtrl', editCategoryCtrl)

editCategoryCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state','$stateParams'];
function editCategoryCtrl($scope, apiService, notificationService, $state, $stateParams) {

    $scope.data = {

    };

    $scope.save = save;

    function save() {
        $("input").prop('disabled', true);
        apiService.put('api/catalogCategory/put', $scope.data,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã cập nhật.');
                $state.go('app.catalog.category');
            }, function (error) {
                $("input").prop('disabled', false);
                notificationService.displayError(error.data.Message);
            });
    }

    function loadCategories() {
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

    function loadDetail() {
        apiService.get('api/catalogCategory/' + $stateParams.id, null, function (result) {
            $scope.data = result.data;
        }, function () {
            console.log('Cannot get data');
        });
    }


    loadCategories();
    loadDetail();
}

