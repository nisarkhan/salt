
userApp.controller('EmployeesCtrl', function ($scope, $rootScope, $routeParams) {

    
    //var id = headers('location').split('/').pop();
    
    $scope.SubmitEmployees = function (data) {
        /* show sucess message */
        $rootScope.$notifications.push({
            type: 'success',
            title: 'Borrower Payment collection.',
            message: 'The borrower payment collection data was posted succesfully.'
        });
    }
  
});