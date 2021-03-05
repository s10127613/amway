var map, featureList, boroughSearch = [], WaterQualityearch = [], birdSearch = [];
var lat, long;

$(window).resize(function () {
    sizeLayerControl();
});

$(document).on("click", ".feature-row", function (e) {
    $(document).off("mouseout", ".feature-row", clearHighlight);
    sidebarClick(parseInt($(this).attr("id"), 10));
});

if (!("ontouchstart" in window)) {
    $(document).on("mouseover", ".feature-row", function (e) {
        var lat = $(this).attr("lat").replace("'", "").replace("'", "");
        var lng = $(this).attr("lng").replace("'", "").replace("'", "");

        highlight.clearLayers().addLayer(L.circleMarker([lat, lng], highlightStyle));
        map.addLayer(highlight);
    });
}

$(document).on("mouseout", ".feature-row", clearHighlight);

$("#about-btn").click(function () {
    $("#aboutModal").modal("show");
    $(".navbar-collapse.in").collapse("hide");
    return false;
});

$("#full-extent-btn").click(function () {
    map.fitBounds(boroughs.getBounds());
    $(".navbar-collapse.in").collapse("hide");
    return false;
});

$("#legend-btn").click(function () {
    $("#legendModal").modal("show");
    $(".navbar-collapse.in").collapse("hide");
    return false;
});

$("#login-btn").click(function () {
    $("#loginModal").modal("show");
    $(".navbar-collapse.in").collapse("hide");
    return false;
});

$("#list-btn").click(function () {
    animateSidebar();
    return false;
});

$("#nav-btn").click(function () {
    $(".navbar-collapse").collapse("toggle");
    return false;
});

$("#sidebar-toggle-btn").click(function () {
    animateSidebar();
    return false;
});

$("#sidebar-hide-btn").click(function () {
    animateSidebar();
    return false;
});

function animateSidebar() {
    $("#sidebar").animate({
        width: "toggle"
    }, 350, function () {
        map.invalidateSize();
    });
}

function sizeLayerControl() {
    $(".leaflet-control-layers").css("max-height", $("#map").height() - 50);
}

function clearHighlight() {
    highlight.clearLayers();
}

function sidebarClick(id) {
    var layer = markerClusters.getLayer(id);
    map.setView([layer.getLatLng().lat, layer.getLatLng().lng], 17);
    layer.fire("click");
    /* Hide sidebar and go to the map on small screens */
    if (document.body.clientWidth <= 767) {
        $("#sidebar").hide();
        map.invalidateSize();
    }
}

    /* Update POI List */
function syncSidebar() {
    /* Empty sidebar features */
    $("#feature-list tbody").empty();

    var Center = map.getCenter();
    var TBKeyword = document.getElementById('TBKeyword');

    /* 水質監測資料 */
    let WqArray = new Array();
    let WqMarkerArray = new Array();
    WaterQuality.eachLayer(function (layer) {
        if (map.hasLayer(WaterQualityLayer)) {
            if (map.getBounds().contains(layer.getLatLng())) {
                WqArray.push(`${layer.feature.id}`);
                WqMarkerArray.push(`${L.stamp(layer)}`);
            }
        }
    });
    if (map.hasLayer(WaterQualityLayer)) {
        $.ajax({
            type: 'POST',
            contentType: "application/json",
            url: "../admin/Viewer_Map.aspx/SetWQTemplate",
            data: JSON.stringify({ 'QArray': WqArray, 'MarkerArray': WqMarkerArray, 'Center_lat': Center.lat, 'Center_lng': Center.lng, 'Keyword': TBKeyword.value}),
            dataType: 'json',
            success: function (data) {
                if (data.d == "") {
                    $("#feature-list tbody").append(`<tr class='feature-row' id='[ID]' lat='[LAT]' lng='[LNG]'>
                                                        <td style='vertical-align: middle;'><img width='16' height='18' src='../Scripts/bootleaf-master/assets/img/icon-pin3-turquoise.png'></td>
                                                        <td class='feature-name'>(無水質監測資料)</td>
                                                        <td style='vertical-align: middle;'><i class='fa fa-chevron-right pull-right'></i></td>
                                                    </tr>`);
                }
                else {
                    $("#loading").show();
                    $("#feature-list tbody").append(`${data.d}`);
                    $("#loading").hide();
                }
            },
        });
    }

    /* 鳥類監測資料POI */
    let BirdArray = new Array();
    let BirdMarkerArray = new Array();
    bird.eachLayer(function (layer) {
        if (map.hasLayer(birdLayer)) {
            if (map.getBounds().contains(layer.getLatLng())) {
                BirdArray.push(`${layer.feature.id}`);
                BirdMarkerArray.push(`${L.stamp(layer)}`);
            }
        }
    });
    if (map.hasLayer(birdLayer)) {
        $.ajax({
            type: 'POST',
            contentType: "application/json",
            url: "../admin/Viewer_Map.aspx/SetBirdTemplate",
            data: JSON.stringify({ 'QArray': BirdArray, 'MarkerArray': BirdMarkerArray, 'Center_lat': Center.lat, 'Center_lng': Center.lng, 'Keyword': TBKeyword.value }),
            dataType: 'json',
            success: function (data) {
                if (data.d == "") {
                    $("#feature-list tbody").append(`<tr class='feature-row' id='[ID]' lat='[LAT]' lng='[LNG]'>
                                                        <td style='vertical-align: middle;'><img width='16' height='18' src='../Scripts/bootleaf-master/assets/img/icon-pin3-red.png'></td>
                                                        <td class='feature-name'>(無鳥類監測資料)</td>
                                                        <td style='vertical-align: middle;'><i class='fa fa-chevron-right pull-right'></i></td>
                                                    </tr>`);
                }
                else {
                    $("#loading").show();
                    $("#feature-list tbody").append(`${data.d}`);
                    $("#loading").hide();
                }
            },
        });
    }

    /* 植物監測資料POI */
    let PlantArray = new Array();
    let PlantMarkerArray = new Array();
    plant.eachLayer(function (layer) {
        if (map.hasLayer(plantLayer)) {
            if (map.getBounds().contains(layer.getLatLng())) {
                PlantArray.push(`${layer.feature.id}`);
                PlantMarkerArray.push(`${L.stamp(layer)}`);
            }
        }
    });
    if (map.hasLayer(plantLayer)) {
        $.ajax({
            type: 'POST',
            contentType: "application/json",
            url: "../admin/Viewer_Map.aspx/SetPlantTemplate",
            data: JSON.stringify({ 'QArray': PlantArray, 'MarkerArray': PlantMarkerArray, 'Center_lat': Center.lat, 'Center_lng': Center.lng, 'Keyword': TBKeyword.value }),
            dataType: 'json',
            success: function (data) {
                if (data.d == "") {
                    $("#feature-list tbody").append(`<tr class='feature-row' id='[ID]' lat='[LAT]' lng='[LNG]'>
                                                        <td style='vertical-align: middle;'><img width='16' height='18' src='../Scripts/bootleaf-master/assets/img/icon-pin3-yellow.png'></td>
                                                        <td class='feature-name'>(無植物監測資料)</td>
                                                        <td style='vertical-align: middle;'><i class='fa fa-chevron-right pull-right'></i></td>
                                                    </tr>`);
                }
                else {
                    $("#loading").show();
                    $("#feature-list tbody").append(`${data.d}`);
                    $("#loading").hide();
                }
            },
        });
    }


    /* 各區POI列表 */
    let BDArray = new Array();
    let BDMarkerArray = new Array();
    BD.eachLayer(function (layer) {
        if (map.hasLayer(BDLayer)) {
            if (map.getBounds().contains(layer.getLatLng())) {
                BDArray.push(`${layer.feature.id}`);
                BDMarkerArray.push(`${L.stamp(layer)}`);
            }
        }
    });  
    if (map.hasLayer(BDLayer)) {
        $.ajax({
            type: 'POST',
            contentType: "application/json",
            url: "../Admin/Viewer_Map.aspx/SetTemplate",
            data: JSON.stringify({ 'QArray': BDArray, 'MarkerArray': BDMarkerArray, 'Center_lat': Center.lat, 'Center_lng': Center.lng, 'Keyword': TBKeyword.value }),
            dataType: 'json',
            success: function (data) {
                $("#feature-list tbody").append(`${data.d}`);
            },
        });
    }
}

function removeLoader() {
    $("#loadingDiv").fadeOut(500, function () {
        // fadeOut complete. Remove the loading div
        $("#loadingDiv").remove(); //makes page more lightweight 
    });
}

/* Basemap Layers */
var EAMP = new L.tileLayer('https://wmts.nlsc.gov.tw/wmts/EMAP/default/GoogleMapsCompatible/{z}/{y}/{x}', {
    attribution: '<a href="https://maps.nlsc.gov.tw/" target="_blank">內政部國土測繪中心</a>版權所有©Copyright 2015',
});

var PHOTO2 = new L.tileLayer('https://wmts.nlsc.gov.tw/wmts/PHOTO2/default/GoogleMapsCompatible/{z}/{y}/{x}.jpg', {
    attribution: '<a href="https://maps.nlsc.gov.tw/" target="_blank">NLSC</a>版權所有©Copyright 2015',
});

/* Overlay Layers */
var B5000 = new L.tileLayer('https://wmts.nlsc.gov.tw/wmts/B5000/default/GoogleMapsCompatible/{z}/{y}/{x}.png', {
});

var MB5000 = new L.tileLayer('https://wmts.nlsc.gov.tw/wmts/MB5000/default/GoogleMapsCompatible/{z}/{y}/{x}.png', {
});

var TOWN = new L.tileLayer('https://wmts.nlsc.gov.tw/wmts/TOWN/default/GoogleMapsCompatible/{z}/{y}/{x}.png', {
});

var CITY = new L.tileLayer('https://wmts.nlsc.gov.tw/wmts/CITY/default/GoogleMapsCompatible/{z}/{y}/{x}.png', {
});

var Village = new L.tileLayer('https://wmts.nlsc.gov.tw/wmts/Village/default/GoogleMapsCompatible/{z}/{y}/{x}.png', {
});

var SCHOOL = new L.tileLayer('https://wmts.nlsc.gov.tw/wmts/SCHOOL/default/GoogleMapsCompatible/{z}/{y}/{x}.png', {
});

var DMAPS = new L.tileLayer('https://landmaps.nlsc.gov.tw/S_Maps/wmts/DMAPS/default/EPSG:3857/{z}/{y}/{x}.png', {
});

//Create a color dictionary based off of subway route_id
var Colors = {

    "NewPond": "#ff3135",
    "Pond": "#fd9a00",
    "RockGet": "#9b8900",
    "bd": "#ff3135",
    "dy": "#009b2e",
    "dh": "#ce06cb",
    "cl": "#fd9a00",
    "ty": "#ccff00",
    "pz": "#9400fd",
    "fh": "#00a0fd",
    "hw": "#bf5c5c",
    "hw": "#aeb579",
    "ym": "#83a680",
    "lt": "#abc9de",
    "gs": "#e381c2",
    "lz": "#47b6de",
    "gi": "#c79f83",
    "TY": "#4a4a4ad4",
};

//var style = {
//    "clickable": true,
//    "fillColor": "#595360",
//    "weight": 1.0,
//    "opacity": 0.3,
//    "fillOpacity": 0.2
//};


//---------------縣市區域界線--------------------------------
var geojsonURL = '../Scripts/bootleaf-master/data/ty.geojson';
var geojsonTileLayer = new L.TileLayer.GeoJSON(geojsonURL, {
    clipTiles: true,
    unique: function (feature) { return feature.id; }
}, {
        style: function (feature) {
            return {
                color: Colors[`TY`],
                weight: 2,
                opacity: 0.8,
                fillOpacity: 0.2,
            };
        }
    }
);

var bdURL = '../Scripts/bootleaf-master/data/area/bd.json';
var BDTileLayer = new L.TileLayer.GeoJSON(bdURL, {
    clipTiles: true,
    unique: function (feature) { return feature.id; }
}, {
        style: function (feature) {
            return {
                color: Colors[`bd`],
                weight: 2,
                opacity: 0.8,
                fillOpacity: 0.2,
            };
        }
    }
);


//---------------縣市區域界線--------------------------------

var highlight = L.geoJson(null);
var highlightStyle = {
    stroke: false,
    fillColor: "#0043ff",
    fillOpacity: 0.5,
    radius: 5
};

/* Single marker cluster layer to hold all clusters */
var markerClusters = new L.MarkerClusterGroup({
    spiderfyOnMaxZoom: true,
    showCoverageOnHover: false,
    zoomToBoundsOnClick: true,
    disableClusteringAtZoom: 18,
    chunkedLoading: true,
});

$('body').append('<div style="" id="loadingDiv"><div class="loader"></div></div>');
var WaterQualityLayer = L.geoJson(null);
var WaterQuality = L.geoJson(null, {
    pointToLayer: function (feature, latlng) {
        return L.marker(latlng, {
            icon: L.icon({
                iconUrl: "../Scripts/bootleaf-master/assets/img/icon-pin3-turquoise.png",
                iconSize: [30, 30],
                iconAnchor: [15, 28],
                popupAnchor: [0, -25]
            }),
            title: feature.properties.tname,
            riseOnHover: true
        });
    },
    onEachFeature: function (feature, layer) {
        if (feature.properties) {
            var content = `<table class='table table-striped table-bordered table-condensed'>
                                <tr><th style="width: 40%;">水溫</th><td>${feature.properties.c}</td></tr>
                                <tr><th style="width: 40%;">溶氧量(DO)(mg/L)</th><td>${feature.properties.DO}</td></tr>
                                <tr><th style="width: 40%;">導電度(EC)(μmho/cm 25℃)</th><td>${feature.properties.ec}</td></tr>
                                <tr><th style="width: 40%;">氨氮(NH3-N)(mg/L)</th><td>${feature.properties.nh3n}</td></tr>
                                <tr><th style="width: 40%;">懸浮固體(SS)(mg/L)</th><td>${feature.properties.ss}</td></tr>
                                <tr><th style="width: 40%;">生化需氧量(BOD)(mg/L)</th><td>${feature.properties.bod}</td></tr>
                                <tr><th style="width: 40%;">硝酸鹽氮(NO3-N)(mg/L)</th><td>${feature.properties.no3n}</td></tr>
                                <tr><th style="width: 40%;">氫離子濃度指數(pH)</th><td>${feature.properties.ph}</td></tr>
                                <tr><th style="width: 40%;">化學需氧量(COD)(mg/L)</th><td>${feature.properties.cod}</td></tr>
                                <tr><th style="width: 40%;">總磷(T-P)(mg/L)</th><td>${feature.properties.tp}</td></tr>
                                <tr><th style="width: 40%;">鹽度(ppt)</th><td>${feature.properties.ppt}</td></tr>
                                <tr><th style="width: 40%;">亞硝酸鹽(NO2)(mg/L)</th><td>${feature.properties.no2}</td></tr>
                                <tr><th style="width: 40%;">總凱氏氮(TKN)(mg/L)</th><td>${feature.properties.tkn}</td></tr>
                                <tr><th style="width: 40%;">溶氧(DOP)(%)</th><td>${feature.properties.dop}</td></tr>
                                <tr><th style="width: 40%;">調查人員</th><td>${feature.properties.inspector}</td></tr>
                                <tr><th style="width: 40%;">調查日期</th><td>${feature.properties.cdate}</td></tr>
                                <tr><th style="width: 40%;">座標(WGS84)</th><td>${feature.geometry.coordinates[1]},${feature.geometry.coordinates[0]}</td></tr>
                           <table>`;
            layer.on({
                click: function (e) {
                    $("#feature-title").html(`${feature.properties.tname}`);
                    $("#feature-info").html(content);
                    $("#featureModal").modal("show");
                    highlight.clearLayers().addLayer(L.circleMarker([feature.geometry.coordinates[1], feature.geometry.coordinates[0]], highlightStyle));
                }
            });
            if (map.hasLayer(birdLayer)) {
                $("#feature-list tbody").append('<tr class="feature-row" id="' + L.stamp(layer) + '" lat="' + layer.getLatLng().lat + '" lng="' + layer.getLatLng().lng + '"><td style="vertical-align: middle;"><img width="16" height="18" src="../Scripts/bootleaf-master/assets/img/icon-pin3-turquoise.png"></td><td class="feature-name">' + layer.feature.properties.tname + '</td><td style="vertical-align: middle;"><i class="fa fa-chevron-right pull-right"></i></td></tr>');
                WaterQualityearch.push({
                    name: layer.feature.properties.name,
                    address: layer.feature.properties.description,
                    source: "WaterQuality",
                    id: L.stamp(layer),
                    lat: layer.feature.geometry.coordinates[1],
                    lng: layer.feature.geometry.coordinates[0]
                });
            }
        }
    }
});
$.ajax({
    type: "POST",
    url: "../admin/Viewer_Map.aspx/getWqJSON",
    data: {},
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        WaterQuality.addData(data);
    }
});

var birdLayer = L.geoJson(null);
var bird = L.geoJson(null, {
    pointToLayer: function (feature, latlng) {
        return L.marker(latlng, {
            icon: L.icon({
                iconUrl: "../Scripts/bootleaf-master/assets/img/icon-pin3-red.png",
                iconSize: [30, 30],
                iconAnchor: [15, 28],
                popupAnchor: [0, -25]
            }),
            title: feature.properties.cname,
            riseOnHover: true
        });
    },
    onEachFeature: function (feature, layer) {
        if (feature.properties) {
            var content = `<table class='table table-striped table-bordered table-condensed'>
                                <tr><th style="text-align: center;width: 20%;">區域</th><td>${feature.properties.tname}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">俗名</th><td>${feature.properties.cname}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">學名</th><td>${feature.properties.sname}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">目名</th><td>${feature.properties.order}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">科名</th><td>${feature.properties.family}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">調查人員</th><td>${feature.properties.inspector}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">調查日期</th><td>${feature.properties.cdate}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">座標(WGS84)</th><td>${feature.geometry.coordinates[1]},${feature.geometry.coordinates[0]}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">測站描述</th><td>${feature.properties.describe}</td></tr>                       
                                <tr><th colspan="2" style="text-align: end;">
                                    <a href='BirdMgt_Edit.aspx?ID=${feature.properties.id}' target='_blank'>詳細資料</a>
                                </th></tr>
                           <table>`;
            layer.on({
                click: function (e) {
                    $("#feature-title").html(`${feature.properties.cname}`);
                    $("#feature-info").html(content);
                    $("#featureModal").modal("show");
                    highlight.clearLayers().addLayer(L.circleMarker([feature.geometry.coordinates[1], feature.geometry.coordinates[0]], highlightStyle));
                }
            });
        }
    }
});
$.ajax({
    type: "POST",
    url: "../admin/Viewer_Map.aspx/getBirdJSON",
    data: {},
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        bird.addData(data);
    }
});

var plantLayer = L.geoJson(null);
var plant = L.geoJson(null, {
    pointToLayer: function (feature, latlng) {
        return L.marker(latlng, {
            icon: L.icon({
                iconUrl: "../Scripts/bootleaf-master/assets/img/icon-pin3-yellow.png",
                iconSize: [30, 30],
                iconAnchor: [15, 28],
                popupAnchor: [0, -25]
            }),
            title: feature.properties.cname,
            riseOnHover: true
        });
    },
    onEachFeature: function (feature, layer) {
        if (feature.properties) {
            var content = `<table class='table table-striped table-bordered table-condensed'>
                                <tr><th style="text-align: center;width: 20%;">區域</th><td>${feature.properties.tname}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">學名</th><td>${feature.properties.sname}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">目名</th><td>${feature.properties.order}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">科名</th><td>${feature.properties.family}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">調查人員</th><td>${feature.properties.inspector}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">調查日期</th><td>${feature.properties.cdate}</td></tr>   
                                <tr><th style="text-align: center;width: 20%;">座標(WGS84)</th><td>${feature.geometry.coordinates[1]},${feature.geometry.coordinates[0]}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">測站描述</th><td>${feature.properties.describe}</td></tr>
                                <tr><th style="text-align: center;width: 20%;">相關資料網站</th><td><a class='url-break' href='http://taibnet.sinica.edu.tw/chi/taibnet_species_detail.php?name_code=${feature.properties.speciesID}' target='_blank'>Taibnet&nbsp;臺灣物種名錄</a></td></tr>
                                <tr><th colspan="2" style="text-align: end;">
                                    <a href='PlantMgt_Edit.aspx?ID=${feature.properties.id}' target='_blank'>詳細資料</a>
                                </th></tr>
                           <table>`;
            layer.on({
                click: function (e) {
                    $("#feature-title").html(`${feature.properties.cname}`);
                    $("#feature-info").html(content);
                    $("#featureModal").modal("show");
                    highlight.clearLayers().addLayer(L.circleMarker([feature.geometry.coordinates[1], feature.geometry.coordinates[0]], highlightStyle));
                }
            });
        }
    }
});
$.ajax({
    type: "POST",
    url: "../admin/Viewer_Map.aspx/getPlantJSON",
    data: {},
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        plant.addData(data);
    }
});

var Pond_Content = `<table class='table table-striped table-bordered table-condensed'>
                                <tr><th style="text-align: justify;width: 30%;">區域</th><td>[TNAME]</td></tr>
                                <tr><th style="text-align: justify;width: 30%;">埤塘編號</th><td>[POOL_NUMB_NEW]</td></tr>
                                <tr><th style="text-align: justify;width: 30%;">濕地保育編號</th><td>[PNumber]</td></tr>
                                <tr><th style="text-align: justify;width: 30%;">埤塘別名</th><td>[NICK_NAME]</td></tr> 
                                <tr><th style="text-align: justify;width: 30%;">座標(WGS84)</th><td>[POS]</td></tr>                      
                                <tr><th colspan="2" style="text-align: center;">
                                    [Result]
                                </th></tr>
                                <tr><th colspan="2" style="text-align: end;">
                                    <a target='_blank' href='https://www.google.com.tw/maps/dir/[SPOS]/[EPOS]/'><i class="fa fa-google-plus-square"></i>&nbsp;Google路徑規劃</a>
                                </th></tr>
                                <tr><th colspan="2" style="text-align: end;">
                                    <a href='QcoMgt_Edit.aspx?ID=[ID]' target='_blank'>詳細資料</a>
                                </th></tr>
                           <table>`;

navigator.geolocation.watchPosition((position) => {
    lat = position.coords.latitude;
    lng = position.coords.longitude;
});

var BDLayer = L.geoJson(null);
var BD = L.geoJson(null, {
    pointToLayer: function (feature, latlng) {
        return L.marker(latlng, {
            icon: L.icon({
                iconUrl: "../Scripts/bootleaf-master/assets/img/icon-pin3-purple.png",
                iconSize: [30, 30],
                iconAnchor: [15, 29],
                popupAnchor: [0, -25]
            }),
            title: `【${feature.properties.TNAME}】${feature.properties.POOL_NUMB_NEW}`,
            riseOnHover: true
        });
    },
    onEachFeature: function (feature, layer) {
        if (feature.properties) {
            layer.on({
                click: function (e) {
                    $("#feature-title").html(`${feature.properties.TNAME}&nbsp;${feature.properties.POOL_NUMB_NEW}`);
                    $("#feature-info").html(Pond_Content.replace('[TNAME]', feature.properties.TNAME)
                        .replace('[DRAWNUMB]', feature.properties.DrawNum)
                        .replace('[POOLNUMB]', feature.properties.POOL_NUMB)
                        .replace('[POOL_NUMB_NEW]', feature.properties.POOL_NUMB_NEW)
                        .replace('[PNumber]', feature.properties.PNumber)
                        .replace('[POS]', `${feature.geometry.coordinates[1]},${feature.geometry.coordinates[0]}`)
                        .replace('[EPOS]', `${feature.geometry.coordinates[1]},${feature.geometry.coordinates[0]}`)
                        .replace('[SPOS]', `${lat},${lng}`)
                        .replace('[ID]', feature.properties.ID)
                        .replace('[NICK_NAME]', feature.properties.NICK_NAME)
                        .replace('[Result]', feature.properties.Result));
                    $("#featureModal").modal("show");
                    highlight.clearLayers().addLayer(L.circleMarker([feature.geometry.coordinates[1], feature.geometry.coordinates[0]], highlightStyle));
                }
            });
        }
    }
});
$.ajax({
    type: "POST",
    url: "../admin/Viewer_Map.aspx/getQiangtangsJSON",
    data: {},
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        BD.addData(data);
    }
});

//埤塘(2851口)圖層
var pondLayer = L.geoJson(null);
var pond = L.geoJson(null, {
    style: function (feature) {
        return {
            color: Colors[`Pond`],
            weight: 2,
            opacity: 0.8,
            fillOpacity: 0.2,
        };
    },
});
$.ajax({
    type: "POST",
    url: "../admin/Viewer_Map.aspx/getPondJSON",
    data: JSON.stringify({ 'Data': `Pond.geojson` }),
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        pond.addData(data);
    }
});

//新增埤塘範圍圖層
var newpondLayer = L.geoJson(null);
var newpond = L.geoJson(null, {
    style: function (feature) {
        return {
            color: Colors[`NewPond`],
            weight: 2,
            opacity: 0.8,
            fillOpacity: 0.2,
        };
    },
});
$.ajax({
    type: "POST",
    url: "../admin/Viewer_Map.aspx/getPondJSON",
    data: JSON.stringify({ 'Data': `NewPond.geojson` }),
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        newpond.addData(data);
    }
});

//RockGet圖層
var rockgetLayer = L.geoJson(null);
var rockget = L.geoJson(null, {
    style: function (feature) {
        return {
            color: Colors[`RockGet`],
            weight: 2,
            opacity: 0.8,
            fillOpacity: 0.2,
        };
    },
});
$.ajax({
    type: "POST",
    url: "../admin/Viewer_Map.aspx/getPondJSON",
    data: JSON.stringify({ 'Data': `RockGet.geojson` }),
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        rockget.addData(data);
    }
});

//WerLand圖層
var werlandLayer = L.geoJson(null);
var werland = L.geoJson(null, {
    style: function (feature) {
        return {
            color: "#5400b3",
            weight: 2,
            opacity: 0.8
        };
    },
});
$.ajax({
    type: "POST",
    url: "../admin/Viewer_Map.aspx/getPondJSON",
    data: JSON.stringify({ 'Data': `WerLand.geojson` }),
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        werland.addData(data);
    }
});

//Eco圖層
var ecoLayer = L.geoJson(null);
var eco = L.geoJson(null, {
    style: function (feature) {
        return {
            color: "#0086b3",
            weight: 2,
            opacity: 0.8
        };
    },
});
$.ajax({
    type: "POST",
    url: "../admin/Viewer_Map.aspx/getPondJSON",
    data: JSON.stringify({ 'Data': `eco.geojson` }),
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        eco.addData(data);
    }
});
setTimeout(removeLoader, 1000); // 4000ms 

map = L.map("map", {
    zoom: 12,
    minZoom: 12,
    center: new L.LatLng(24.993057, 121.300911),
    layers: [EAMP, markerClusters],
    zoomControl: false,
    attributionControl: false
});

/* Layer control listeners that allow for a single markerClusters layer */
map.on("overlayadd", function (e) {
    $('body').append('<div style="" id="loadingDiv"><div class="loader"></div></div>'); 
    if (e.layer === WaterQualityLayer) {
        markerClusters.addLayer(WaterQuality);
        syncSidebar();
    }
    if (e.layer === birdLayer) {
        markerClusters.addLayer(bird);
        syncSidebar();
    }
    if (e.layer === plantLayer) {
        markerClusters.addLayer(plant);
        syncSidebar();
    }
    if (e.layer === BDLayer) {
        markerClusters.addLayer(BD);
        syncSidebar();
    }
    map.setView([24.968474451099286, 121.21868133544923], 12);
    setTimeout(removeLoader, 500); // 500ms 
});

map.on("overlayremove", function (e) {
    if (e.layer === WaterQualityLayer) {
        markerClusters.removeLayer(WaterQuality);
        syncSidebar();
    }
    if (e.layer === birdLayer) {
        markerClusters.removeLayer(bird);
        syncSidebar();
    }
    if (e.layer === plantLayer) {
        markerClusters.removeLayer(plant);
        syncSidebar();
    }
    if (e.layer === BDLayer) {
        markerClusters.removeLayer(BD);
        syncSidebar();
    }
});

map.locate({ setView: true, maxZoom: 12 });

/* Filter sidebar feature list to only show features in current map bounds */
map.on("moveend", function (e) {
    syncSidebar();
});

/* Clear feature highlight when map is clicked */
var marker = null, marker02 = null;
map.on("click", function (e) {
    highlight.clearLayers();
    if (marker) map.removeLayer(marker);
    marker = L.marker(e.latlng, {
        icon: L.icon({
            iconUrl: "../Scripts/bootleaf-master/assets/img/icon-pin3-purple.png",
            iconSize: [30, 30],
            iconAnchor: [15, 27],
            popupAnchor: [0, -25]
        }),
    }).addTo(map).bindPopup(e.latlng + '<br/>' + e.layerPoint).openPopup();
});

/* Attribution control */
function updateAttribution(e) {
    $.each(map._layers, function (index, layer) {
        if (layer.getAttribution) {
            $("#attribution").html((layer.getAttribution()));
        }
    });
}
map.on("layeradd", updateAttribution);
map.on("layerremove", updateAttribution);

var attributionControl = L.control({
    position: "bottomright"
});
attributionControl.onAdd = function (map) {
    var div = L.DomUtil.create("div", "leaflet-control-attribution");
    div.innerHTML = "<span class='hidden-xs'><a href='https://leafletjs.com/'>Leaflet</a> | </span><a href='#' onclick='$(\"#attributionModal\").modal(\"show\"); return false;'>NLSC</a>&nbsp;版權所有&nbsp;©Copyright 2015</a>";
    return div;
};
map.addControl(attributionControl);

var zoomControl = L.control.zoom({
    position: "bottomright"
}).addTo(map);

/* GPS enabled geolocation control set to follow the user's location */
var locateControl = L.control.locate({
    position: "bottomright",
    drawCircle: false,
    follow: true,
    setView: true,
    keepCurrentZoomLevel: true,
    markerStyle: {
        weight: 1,
        opacity: 0.8,
        fillOpacity: 0.8
    },
    circleStyle: {
        weight: 1,
        clickable: false,
    },
    icon: "fa fa-location-arrow",
    metric: false,
    strings: {
        title: "目前位置",
    },
    locateOptions: {
        maxZoom: 18,
        watch: true,
        enableHighAccuracy: true,
        maximumAge: 10000,
        timeout: 10000
    }
}).addTo(map);

/* Larger screens get expanded layer control and visible sidebar */
if (document.body.clientWidth <= 767) {
  var isCollapsed = true;
} else {
  var isCollapsed = false;
}

var baseLayers = {
    "<span>臺灣通用電子地圖</span>": EAMP,
    "<span>正射影像圖(通用版)</span>": PHOTO2,
};

var groupedOverlays = {
    "生態監測資料": {
        "水質監測": WaterQualityLayer,
        "鳥類監測": birdLayer,
        "水生植物監測": plantLayer,
        "埤塘": BDLayer,
    },
    "埤塘範圍資料": {
        "埤塘(2851口)": pond,
        "新增埤塘範圍": newpond,
        "農田水利署石門管理處-埤塘": rockget,
        "保育利用計畫重要濕地(340口)": werland,
        "生態調查範圍(140口)": eco,
    },
    "圖層設定": {
        "1/5000基本地形圖": B5000,
        "1/5000圖幅框": MB5000,
        "鄉鎮區界": TOWN,
        "村里界": Village,
        "各級學校範圍圖": SCHOOL,
        "桃園市界": geojsonTileLayer,
        "地籍圖磚": DMAPS,
    },
};

var layerControl = L.control.groupedLayers(baseLayers, groupedOverlays, {
    //collapsed: isCollapsed
    collapsed: true
}).addTo(map);


/* Highlight search box text on click */
$("#searchbox").click(function () {
  $(this).select();
});

/* Prevent hitting enter from refreshing the page */
$("#searchbox").keypress(function (e) {
  if (e.which == 13) {
    e.preventDefault();
  }
});

$("#featureModal").on("hidden.bs.modal", function (e) {
  $(document).on("mouseout", ".feature-row", clearHighlight);
});

/* 顯示目前座標 */
L.control.mapCenterCoord().addTo(map);

/* Google 地理位置轉換 */
function AddressToPos() {
    var geocoder = new google.maps.Geocoder();
    var TB_Searchbox = document.getElementById('searchbox');
    var Btn_Search = document.getElementById('BtnSearch');

    if (TB_Searchbox.value != ``) {
        Btn_Search.disabled = true;
        setTimeout(function () {
            Btn_Search.disabled = false;
        }, 3000); //停頓3s 防止重複送出

        $.ajax({
            type: "POST",
            url: "../Default/Maps.aspx/Is_NICK",
            data: JSON.stringify({ 'nickname': TB_Searchbox.value }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //搜尋資料庫是否符合的埤塘別名，並回傳座標
                if (data.d != ``) {
                    var array = data.d.split(',');
                    addMarker(array[0], array[1]);
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "../Default/Maps.aspx/SearchLog",
                        data: JSON.stringify({ 'QueryWord': TB_Searchbox.value }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            //搜尋歷史資料庫是否有相關地理位置。
                            if (data.d != ``) {
                                var array2 = data.d.split(',');
                                addMarker(array2[0], array2[1]);
                            }
                            else {
                                //上述資料庫若無資料才使用Google搜尋
                                geocoder.geocode({ 'address': TB_Searchbox.value }, function (results, status) {
                                    if (status == 'OK') {
                                        let lat = `${results[0].geometry.location.lat()}`;
                                        let lng = `${results[0].geometry.location.lng()}`;
                                        $.ajax({
                                            type: "POST",
                                            url: "../Default/Maps.aspx/SaveLog",
                                            data: JSON.stringify({ 'QueryWord': TB_Searchbox.value, 'lat': lat, 'lng': lng }),
                                            contentType: "application/json; charset=utf-8",
                                            dataType: "json",
                                            success: function () {
                                                addMarker(lat, lng);
                                            }
                                        });
                                    }
                                    else {
                                        alert(`搜尋失敗，請重新輸入有效地址或名稱。`)
                                    }
                                });
                            }
                        }
                    });
                }
            }
        });
    }
    else {
        alert(`請輸入地址或埤塘別名 `)
    }
}

function addMarker(lat, lng) {
    if (marker02) map.removeLayer(marker02);
    map.setView([lat, lng], 17);
    L.marker([lat, lng], {
        icon: L.icon({
            iconUrl: "../Scripts/bootleaf-master/assets/img/icon-pin3-red.png",
            iconSize: [30, 30],
            iconAnchor: [15, 27],
            popupAnchor: [0, -25]
        }),
    }).addTo(map);
}

/* 帶入中心座標 */
function GetLocation() {
    //document.getElementsByClassName('undefined')[0].value = `${map.getCenter().lat},${map.getCenter().lng}`;
    document.getElementById('searchbox').value = `${map.getCenter().lat},${map.getCenter().lng}`;
}

function Remove() {
    document.getElementById('searchbox').value = ``;
}

/* 關鍵字 */
$("#TBKeyword").change(function () {
    syncSidebar();
});



