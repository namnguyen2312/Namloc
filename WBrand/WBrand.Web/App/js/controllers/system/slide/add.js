//chart.js
angular
    .module('app')
    .controller('addSlideShowCtrl', addSlideShowCtrl)

addSlideShowCtrl.$inject = ['$scope', 'apiService', 'notificationService', '$state'];
function addSlideShowCtrl($scope, apiService, notificationService, $state) {

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

    function loadPosition() {
        apiService.get('api/slideShow/getAllPosition', null, function (result) {
            $scope.position = result.data;
        }, function () {
            console.log('Cannot get data');
        });
    }

    function save() {
        $("input").prop('disabled', true);
        apiService.post('api/slideShow/Add', $scope.data,
            function (result) {
                notificationService.displaySuccess($scope.data.Name + ' đã thêm mới.');
                $state.go('app.system.slideShow');
            }, function (error) {
                $("input").prop('disabled', false);
                notificationService.displayError(error.data.Message);
            });
    }

    loadPosition();

}