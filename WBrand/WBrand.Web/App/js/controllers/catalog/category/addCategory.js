//chart.js
angular
    .module('app')
    .controller('addCategoryCtrl', addCategoryCtrl)

addCategoryCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state'];
function addCategoryCtrl($scope, apiService, notificationService, $state) {

    $scope.data = {
        
    };

    $scope.save = save;

    function save() {
        $("input").prop('disabled', true);
        apiService.post('api/catalogCategory/Add', $scope.data,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã được thêm mới.');
                $state.go('app.catalog.category');
            }, function (error) {
                $("input").prop('disabled', false);
                notificationService.displayError(error.data.Message);
            });
    }

    function loadCategories() {
        apiService.get('api/catalogCategory/getAll', null, function (result) {
            $scope.categories = result.data;
        }, function () {
            console.log('Cannot get data');
        });
    }

    loadCategories();
}

