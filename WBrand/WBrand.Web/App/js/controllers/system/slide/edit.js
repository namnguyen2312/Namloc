//chart.js
angular
    .module('app')
    .controller('editSlideShowCtrl', editSlideShowCtrl)

editSlideShowCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams'];
function editSlideShowCtrl($scope, apiService, notificationService, $state, $stateParams) {

    $scope.data = {

    };

    $scope.save = save;
    $scope.chooseImage = chooseImage;

    function save() {
        $("input").prop('disabled', true);
        apiService.put('api/slideShow/put', $scope.data,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã thêm mới.');
                $state.go('app.system.slideShow');
            }, function (error) {
                $("input").prop('disabled', false);
                notificationService.displayError(error.data.Message);
            });
    }

    function chooseImage(item) {
        var finder = new CKFinder();
        finder.selectActionFunction = function (fileUrl) {
            $scope.$apply(function () {
                $scope.data.Image = fileUrl;
            });
        };
        finder.popup();
    }

    function loadDetail() {
        apiService.get('api/slideShow/' + $stateParams.id, null, function (result) {
            $scope.data = result.data;
        }, function () {
            console.log('Cannot get data');
        });
    }

    function loadPosition() {
        apiService.get('api/slideShow/getAllPosition', null, function (result) {
            $scope.positions = result.data;
        }, function () {
            console.log('Cannot get data');
        });
    }

    loadPosition();
    loadDetail();
}