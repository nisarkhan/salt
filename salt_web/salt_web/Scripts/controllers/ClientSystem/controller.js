
userApp.controller('application.clientsystems', function ($scope, $rootScope, $routeParams, $storage) {

    var data = $storage.get('application.clientsystems');
    //var id = headers('location').split('/').pop();
    
    $scope.SubmitEmployees = function (data) {
        /* show sucess message */
        $rootScope.$notifications.push({
            type: 'success',
            title: 'Borrower Payment collection.',
            message: 'The borrower payment collection data was posted succesfully.'
        });
    }
    
    $scope.save = function (data) {

        $scope.client_system = angular.copy(data);
        $storage.put('application.clientsystems', $scope.client_system);
        debugger;

    }

    $rootScope.$on('$viewContentLoaded', function () {
        debugger
        $templateCache.removeAll();

       
    });

   
  
});