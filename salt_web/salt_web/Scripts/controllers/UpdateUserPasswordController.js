
userApp.controller('UpdateUserPasswordCtrl', function ($scope, $http, $window) {
    $scope.UpdateUserPassword = { 'UserName': '', 'CurrentPassword': '', 'NewPassword': '', 'ConfirmNewPassword': '', 'IsSuccess': false };
    $scope.alerts = [];
    $scope.addAlert = function (mtype, message) {
        $scope.alerts.push({ type: mtype, msg: message });
    };
    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };

    $scope.text = $window.updateUserPasswordViewModel.Text;


    $scope.init = function () {
        var userId = $('#UserId').val();
        $http.get('/api/getuserbyid/' + userId).success(function (data) {
            $scope.UpdateUserPassword.UserName = data.UserName;
            $scope.UpdateUserPassword.CurrentPassword = data.CurrentPassword;
            $scope.UpdateUserPassword.NewPassword = data.NewPassword;
            $scope.UpdateUserPassword.ConfirmNewPassword = data.ConfirmNewPassword;
        }).error(function (data, status) {
            $scope.addAlert('danger', 'Failed to ' + ': ' + status);
        });
    };
    //$scope.SubmitProfile = function () {
    //    $scope.UpdateUserPassword.UserId = $('#UserId').val();
    //    $http.put('/api/updateuserbyid', $scope.UpdateUserPassword).success(function (data) {
    //        if ($scope != null && $scope.alerts != null && $scope.alerts.length > 0) {
    //            $scope.closeAlert(0);
    //        }
    //        if (data.IsSuccess == true) {
    //            $scope.updateUser.IsSuccess = data.IsSuccess;
    //            $scope.addAlert('success', 'Profile update successfully.');
    //        }
    //        else {
    //            $scope.addAlert('danger', 'Not able to update profile.');
    //        }
    //    }).error(function (data, status) {
    //        $scope.addAlert('danger', 'Failed to ' + status);
    //    });
    //};
});