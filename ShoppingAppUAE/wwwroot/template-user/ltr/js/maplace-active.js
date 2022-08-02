$(function() {
               
    var LocsA = [
        {
            lat: 25.286911305981732,
            lon: 55.449431372387885,
            title: 'Location 1',
            html: ['<div class="ltn__map-item">',
                '<h3 class="ltn__location-name">Almohandes Car Scanner</h3>',
                '<h5 class="ltn__location-single-info"><i class="fas fa-map-marked-alt"></i>Industrial Area - Industrial Area 17 - Sharjah - United Arab Emirates </h5>',
                '<h4 class="ltn__location-single-info"><i class="fas fa-phone-volume"></i>+971501679410</h4>',
                '<div class="btn-wrapper">',
                '</div>',
                '</div>'
            ].join(''),
            icon: '/template-user/ltr/img/icons/map-marker-2.png',
            animation: google.maps.Animation.BOUNCE
        },
    ];
            new Maplace({
                locations: LocsA,
                controls_on_map: true,
                map_options: {
                    zoom: 13,
                    scrollwheel: false,
                    stopover: true
                },
                stroke_options: {
                    strokeColor: '#f10', 
                    strokeOpacity: 0.8, 
                    strokeWeight: 2, 
                    fillColor: '#f10', 
                    fillOpacity: 0.4
                }
            }).Load();

        });