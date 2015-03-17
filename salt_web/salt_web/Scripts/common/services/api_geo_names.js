'use strict';
/* Application Service */
userApp.factory('$apigeonames', function ($resource, $response, $path) {

    var $apigeonames = $resource('http://api.geonames.org/countryInfoJSON?username=design1online');

            

    $apigeonames.prototype.$options = function () {

                    return '<div class="table-buttons">' +
                       '<a class="btn btn-white btn-sm" ng-click="open($event)"> <i class="fa fa-pencil"></i> Edit</a>' +
                       '<a class="btn btn-white btn-sm" ng-click="remove($event)"> <i class="fa fa-trash-o"></i> Remove</a>' +
                     '</div>';
                }


    return $apigeonames;
  });


