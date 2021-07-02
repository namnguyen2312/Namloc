//chart.js
angular
    .module('app')
    .controller('editUserCtrl', editUserCtrl)

editUserCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$ngBootbox', '$stateParams','$location'];
function editUserCtrl($scope, apiService, notificationService, $ngBootbox, $stateParams, $location) {

    $scope.isUpdate = true;
    $scope.account = {}
    $scope.save = save;

    function save() {
        $("input").prop('disabled', true);
        apiService.put('/api/appUser/update', $scope.account, addSuccessed, addFailed);
    }
    function loadDetail() {
        apiService.get('/api/appUser/detail/' + $stateParams.id, null,
            function (result) {
                $scope.account = result.data;
            },
            function (result) {
                notificationService.displayError(result.data);
            });
    }

    function addSuccessed() {
        notificationService.displaySuccess($scope.account.UserName + ' đã được cập nhật thành công.');

        $location.url('userList');
    }
    function addFailed(response) {
        $("input").prop('disabled', false);
        notificationService.displayError(response.data.Message);
        //notificationService.displayErrorValidation(response);
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
    loadDetail();
}

