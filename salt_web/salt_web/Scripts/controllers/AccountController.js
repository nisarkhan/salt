
userApp.controller('AccountForgotCtrl', function ($scope, $http, $window) {
   
    $scope.forgotPassword = { 'UserName': '', 'IsSuccess': false };
    $scope.alerts = [];
    $scope.addAlert = function (mtype, message) {
        $scope.alerts.push({ type: mtype, msg: message });
    };
    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };

    $scope.SubmitForgotPassword = function () {
        
        $http.post('/api/forgotpassword', $scope.forgotPassword).success(function (data) {
            if (data.IsSuccess == true) {
                window.location.href = 'ForgotPasswordSuccess/' + data.Token;
            }
            else {
                if ($scope != null && $scope.alerts != null && $scope.alerts.length > 0) {
                    $scope.closeAlert(0);
                }
                $scope.addAlert('danger', 'Invalid user');
            }
        }).error(function (data, status) {
            if ($scope != null && $scope.alerts != null && $scope.alerts.length > 0) {
                $scope.closeAlert(0);
            }
            $scope.addAlert('danger', 'Failed to ' + $scope.forgotPassword.UserName + ': ' + status);
        });
    };
});

userApp.directive('pwCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            elem.add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    var v = elem.val() === $(firstPassword).val();
                    ctrl.$setValidity('pwmatch', v);
                    if (scope != null && scope.alerts != null && scope.alerts.length > 0) {
                        scope.closeAlert(0);
                    }
                    if (v == false) {
                        scope.addAlert('danger', 'Password not matched');
                    }
                });
            });
        }
    }
}]);

userApp.controller('PasswordResetCtrl', function ($scope, $http, $window) {
    $scope.resetPassword = { 'Password': '', 'ConfirmPassword': '', 'Token': '', 'IsSuccess': false, 'IsTokenExpired': false };
    $scope.alerts = [];
    $scope.addAlert = function (mtype, message) {
        $scope.alerts.push({ type: mtype, msg: message });
    };
    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };
    $scope.init = function () {
        $scope.resetPassword.Token = $('#Token').val();
        $http.post('/api/validatetoken', $scope.resetPassword).success(function (data) {
            $scope.resetPassword.IsTokenExpired = data.IsTokenExpired;
        }).error(function (data, status) {
            $scope.resetPassword.IsTokenExpired = true;
        });
    };
    $scope.SubmitResetPassword = function () {
        if ($scope.alerts != null && $scope.alerts.length == 0) {
            $http.put('/api/resetpassword', $scope.resetPassword).success(function (data) {
                $scope.resetPassword.IsSuccess = data.IsSuccess;
            }).error(function (data, status) {
                $scope.addAlert('danger', 'Failed to ' + $scope.resetPassword.UserName + ': ' + status);
            });
        }
    };

});

userApp.controller('AccountMyProfileCtrl', function ($scope, $http, $window) {
    $scope.updateUser = { 'FirstName': '', 'LastName': '', 'Email': '', 'OrganizationName': '', 'OrganizationId': 0, 'IsSuccess': false };
    $scope.alerts = [];
    $scope.addAlert = function (mtype, message) {
        $scope.alerts.push({ type: mtype, msg: message });
    };
    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };
    $scope.init = function () {
        var userId = $('#UserId').val();
        $http.get('/api/getuserbyid/' + userId).success(function (data) {
            $scope.updateUser.FirstName = data.FirstName;
            $scope.updateUser.LastName = data.LastName;
            $scope.updateUser.Email = data.Email;
            $scope.updateUser.OrganizationId = data.OrganizationId;
            $scope.updateUser.OrganizationName = data.OrganizationName;
        }).error(function (data, status) {
            $scope.addAlert('danger', 'Failed to ' + ': ' + status);
        });
    };
    $scope.SubmitProfile = function () {
        $scope.updateUser.UserId = $('#UserId').val();
        $http.put('/api/updateuserbyid', $scope.updateUser).success(function (data) {
            if ($scope != null && $scope.alerts != null && $scope.alerts.length > 0) {
                $scope.closeAlert(0);
            }
            if (data.IsSuccess == true) {
                $scope.updateUser.IsSuccess = data.IsSuccess;
                $scope.addAlert('success', 'Profile update successfully.');
            }
            else {
                $scope.addAlert('danger', 'Not able to update profile.');
            }
        }).error(function (data, status) {
            $scope.addAlert('danger', 'Failed to ' + status);
        });
    };
});