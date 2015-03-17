'use strict';
/* Service */

userApp.factory('$test', function ($resource, $response, $path) {

    var $test = $resource('https://itunes.apple.com/search?term=apple', {});
    return $test;

});


userApp.factory('$billinginformation', function ($resource, $response, $path) {

    var $billinginformation = $resource('/BillingInformation/GetBillingInformationList', {},
        {
            GetBillingInformationList:
                {
                    url: '/BillingInformation/GetBillingInformationList',
                    isArray: true,
                    method: 'GET'
                }
        });

    $billinginformation.prototype.$options = function () {
        return '<div class="table-buttons">' +
            '<a class="btn btn-white btn-sm" ng-click="open($event)"> <i class="fa fa-pencil"></i> Edit </a>' +
            '<a class="btn btn-white btn-sm" ng-click="open($event)"> <i class="fa fa-pencil"></i> New </a>' +
            '<a class="btn btn-white btn-sm" ng-click="open($event)"> <i class="fa fa-pencil"></i> Collection</a>' +
            '<a class="btn btn-white btn-sm" ng-click="open($event)"> <i class="fa fa-pencil"></i> Vendor List</a>' 
        '</div>';
    }
    return $billinginformation;
});

userApp.factory('$employee', function ($resource, $response, $path) {

    var $employee = $resource('/Employee/GetEmployeeList', {},
        {
            GetEmployeeList:
                {
                    url: '/Employee/GetEmployeeList',
                    isArray: true,
                    method: 'GET'
                }
        });

    $employee.prototype.$options = function () {
        return '<div class="table-buttons">' +
            '<a class="btn btn-white btn-sm" ng-click="open($event)"> <i class="fa fa-pencil"></i> Update</a>'
        '</div>';
    }
    return $employee;
});

userApp.factory('$jobtracking', function ($resource, $response, $path) {

    var $jobtracking = $resource('/JobTracking/GetJobTrackingList', {},
        {
            GetJobTrackingList:
                {
                    url: '/JobTracking/GetJobTrackingList',
                    isArray: true,
                    method: 'GET'
                }
        });

    $jobtracking.prototype.$options = function () {
        return '<div class="table-buttons">' +
            '<a class="btn btn-white btn-sm" ng-click="open($event)"> <i class="fa fa-pencil"></i> Info</a>'
        '</div>';
    }
    return $jobtracking;
});

userApp.factory('$vendor', function ($resource, $response, $path) {
     
    var $vendor = $resource('/Vendor/GetVendorList', {},
        {
            GetVendorList:
                {
                    url: '/Vendor/GetVendorList',
                    isArray: true, 
                    method: 'GET'
                }
        });

    $vendor.prototype.$options = function ()
    {
        return '<div class="table-buttons">' +
            '<a class="btn btn-white btn-sm" ng-click="open($event)"> <i class="fa fa-pencil"></i> Edit</a>' 
            '</div>';
    }
    return $vendor;
});

userApp.factory('$client', function ($resource, $response, $path) {

    var $client = $resource('/Client/GetClientUpdateList', {},
        {
            GetVendorList:
                {
                    url: '/Client/GetClientUpdateList',
                    isArray: true,
                    method: 'GET'
                }
        });

    $client.prototype.$options = function () {
        return '<div class="table-buttons">' +
            '<a class="btn btn-white btn-sm" ng-click="open($event)"> <i class="fa fa-pencil"></i> Edit</a>'
        '</div>';
    }
    return $client;
});

 

userApp.factory('$salt_client', function ($resource, $response, $path) {

    var $salt_client = $resource('/Clients/GetClientsList', {},
        {
            GetClientsList:
                {
                    url: '/Clients/GetClientsList',
                    isArray: true,
                    method: 'GET'
                }
        });

    $salt_client.prototype.$options = function () {
        return '<div class="table-buttons">' +
            '<a class="btn btn-white btn-sm" ng-click="open($event)"> <i class="fa fa-pencil"></i> Edit</a>' +
            '<a class="btn btn-white btn-sm" ng-click="remove($event)"> <i class="fa fa-trash-o"></i> Remove</a>' +
            '</div>';
    }


    return $salt_client;
});



