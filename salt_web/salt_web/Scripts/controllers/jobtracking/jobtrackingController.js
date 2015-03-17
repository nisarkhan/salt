
userApp.controller('JobTrackingCtrl', function ($scope, $location, $rootScope, $routeParams, $jobtracking) {


    $jobtracking.GetJobTrackingList(function (response)
    {
         $scope.grids.jobtracking.$data(response);
    });


    /* review application */
    $scope.open = function (e) {
     debugger
     var data = $scope.grids.jobtracking.$get(e);
        //$location.path('Clients/Edit/' + data.UserId);// + (section ? '/' + section : ''));
        window.location.href = 'JobTracking/Edit/' + data.CustomerID;
    };
     
  
});