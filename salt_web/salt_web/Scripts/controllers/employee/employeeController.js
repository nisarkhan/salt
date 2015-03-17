
userApp.controller('EmployeeCtrl', function ($scope, $location, $rootScope, $routeParams, $employee)
{
    $employee.GetEmployeeList(function (response)
    {
        $scope.grids.employee.$data(response);
    });


    /* review application */
    $scope.open = function (e) {
     debugger
     var data = $scope.grids.employee.$get(e);
        window.location.href = 'Employee/Edit/' + data.UserId;
    };
     
  
});