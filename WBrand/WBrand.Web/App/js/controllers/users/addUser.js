//chart.js
angular
    .module('app')
    .controller('addUserCtrl', addUserCtrl)

addUserCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$location'];
function addUserCtrl($scope, apiService, notificationService, $location) {

    $scope.isUpdate = false;
    $scope.account = {
        IsActive: true,
        Groups: []
    };

    $scope.save = save;

    function save() {
        $("input").prop('disabled', true);
        apiService.post('/api/appUser/create', $scope.account, addSuccessed, addFailed);
    }

    function addSuccessed() {
        notificationService.displaySuccess($scope.account.FullName + ' đã được thêm mới.');
        $location.url('userList');
    }
    function addFailed(response) {
        $("input").prop('disabled', false);
        notificationService.displayError(response.data.Message);
    }

    function loadGroups() {
        apiService.get('/api/appGroup/all',
            null,
            function (response) {
                $scope.groups = response.data;
            }, function (response) {
                notificationService.displayError('Không tải được danh sách nhóm.');
            });
    }

    loadGroups();
}

