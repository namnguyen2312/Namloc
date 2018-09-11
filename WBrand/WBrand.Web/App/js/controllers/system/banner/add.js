//chart.js
angular
    .module('app')
    .controller('addBannerCtrl', addBannerCtrl)

addBannerCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state'];
function addBannerCtrl($scope, apiService, notificationService, $state) {

    $scope.data = {
    };

    $scope.chooseImage = chooseImage;
    $scope.save = save;

    function chooseImage(item) {
        var finder = new CKFinder();
        finder.selectActionFunction = function (fileUrl) {
            $scope.$apply(function () {
                $scope.data.Image = fileUrl;
            });
        };
        finder.popup();
    }

    function save() {
        $("input").prop('disabled', true);
        apiService.post('api/banner/Add', $scope.data,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã thêm mới.');
                $state.go('app.system.banner');
            }, function (error) {
                $("input").prop('disabled', false);
                notificationService.displayError(error.data.Message);
            });
    }

}