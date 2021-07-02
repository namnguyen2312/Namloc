//chart.js
angular
    .module('app')
    .controller('addRoleGroupCtrl', addRoleGroupCtrl)

addRoleGroupCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$location'];
function addRoleGroupCtrl($scope, apiService, notificationService, $location) {

    $scope.group = {
        ID: 0,
        Roles: []
    }

    $scope.save = save;

    function save() {
        apiService.post('/api/appGroup/create', $scope.group, addSuccessed, addFailed);
    }

    function addSuccessed() {
        notificationService.displaySuccess($scope.group.Name + ' đã được thêm mới.');

        $location.url('roleGroupList');
    }
    function addFailed(response) {
        notificationService.displayError(response.data.Message);
        notificationService.displayErrorValidation(response);
    }
    function loadRoles() {
        apiService.get('/api/appRole/all',
            null,
            function (response) {
                $scope.roles = response.data;
            }, function (response) {
                notificationService.displayError('Không tải được danh sách quyền.');
            });

    }

    loadRoles();
}

