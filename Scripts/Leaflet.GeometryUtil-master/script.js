var llPolyline1 = [
        [39.76116, -105.03702],
        [39.76684, -105.01608],
        [39.76116, -104.95874]],
    llPolyline2 = [
        [39.7572, -105.04217],
        [39.74783, -105.04698],
        [39.7308, -105.0432],],
    llSegment = [
        [39.73397, -105.03256],
        [39.73397, -104.93681]],
    llPolygon1 = [
        [[39.75324, -104.97934],
        [39.75879, -104.96681],
        [39.75379, -104.94681],
        [39.74379, -104.93681],
        [39.74348, -104.97711]], // outer ring
        [[39.75245, -104.97471],
        [39.7547, -104.9663],
        [39.75206, -104.95171],
        [39.74849, -104.94965],
        [39.74744, -104.97385]] // hole
    ],

    _map = L.map('map').setView(/*[51.505, -0.09]*/[39.74739, -105], 13),
    polygon1 = L.polygon(llPolygon1, { color: 'blue', className: 'polygon1' }).addTo(_map),
    polyline1 = L.polyline(llPolyline1, { color: 'blue', className: 'polyline1' }).addTo(_map),
    polyline2 = L.polyline(llPolyline2, { color: 'blue', className: 'polyline2' }).addTo(_map),
    segment = L.polyline(llSegment, { color: 'red', className: 'segment' }).addTo(_map),
    marker = null,
    markerClosestPolygon1 = null,
    markerClosestPolyline1 = null,
    markerClosestPolyline2 = null,
    markerClosestSegment = null;


var bicycle = L.geoJson([bicycleRental, campus], {

    style: function (feature) {
        return feature.properties && feature.properties.style;
    },

    pointToLayer: function (feature, latlng) {
        return L.circleMarker(latlng, {
            radius: 8,
            fillColor: "#ff7800",
            color: "#000",
            weight: 1,
            opacity: 1,
            fillOpacity: 0.8
        });
    }
}).addTo(_map),

    freebusLayer = L.geoJson(freeBus, {

        filter: function (feature, layer) {
            if (feature.properties) {
                // If the property "underConstruction" exists and is true, return false (don't render features under construction)
                return feature.properties.underConstruction !== undefined ? !feature.properties.underConstruction : true;
            }
            return false;
        }

    }).addTo(_map),

    coorsLayer = L.geoJson(coorsField, {

        pointToLayer: function (feature, latlng) {
            return L.marker(latlng, { className: 'marker' });
        }

    }).addTo(_map);

 console.log("latlngs of polygon1")
 console.log(polygon1.getLatLngs())
 console.log("latlngs of polyline1")
 console.log(polyline1.getLatLngs())
 console.log("latlngs of polyline2")
 console.log(polyline2.getLatLngs())

function init() {
    if (marker) _map.removeLayer(marker);
    if (markerClosestPolygon1) _map.removeLayer(markerClosestPolygon1);
    if (markerClosestPolyline1) _map.removeLayer(markerClosestPolyline1);
    if (markerClosestPolyline2) _map.removeLayer(markerClosestPolyline2);
    if (markerClosestSegment) _map.removeLayer(markerClosestSegment);

    polygon1.setStyle({ color: 'blue' });
    polyline1.setStyle({ color: 'blue' });
    polyline2.setStyle({ color: 'blue' });

    document.getElementById('closestLayer').innerHTML = '';
    document.getElementById('closestLayerSnap').innerHTML = '';
}

L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
    attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
}).addTo(_map);

_map.on('click', function (e) {
    init();
    marker = L.marker(e.latlng).addTo(_map).bindPopup(e.latlng + '<br/>' + e.layerPoint).openPopup();

    var p_vertices = document.getElementById('p_vertices').checked,
        p_tolerance = document.getElementById('p_tolerance').value !== '' ? parseInt(document.getElementById('p_tolerance').value) : Infinity,
        p_withVertices = document.getElementById('p_withVertices').checked,
        closestPointToPolygon1 = L.GeometryUtil.closest(_map, polygon1, e.latlng, p_vertices),
        closestPointToPolyline1 = L.GeometryUtil.closest(_map, polyline1, e.latlng, p_vertices),
        closestPointToPolyline2 = L.GeometryUtil.closest(_map, polyline2, e.latlng, p_vertices),
        closestLayer = L.GeometryUtil.closestLayer(_map, bicycle.getLayers().concat(coorsLayer.getLayers(), polyline1, polyline2, polygon1), e.latlng),
        closestLayerSnap = L.GeometryUtil.closestLayerSnap(_map, [polyline1, polygon1, polyline2], e.latlng, p_tolerance, p_withVertices),
        closestOnSegment = L.GeometryUtil.closestOnSegment(_map, e.latlng, llSegment[0], llSegment[1]);

    // display the closest points
    markerClosestPolygon1 = L.marker(closestPointToPolygon1).addTo(_map).bindPopup('Closest point on polygon1');
    markerClosestPolyline1 = L.marker(closestPointToPolyline1).addTo(_map).bindPopup('Closest point on polyline1');
    markerClosestPolyline2 = L.marker(closestPointToPolyline2).addTo(_map).bindPopup('Closest point on polyline2');

    // change the color of closest layer
    if (typeof closestLayer.layer.setStyle === 'function') {
        closestLayer.layer.setStyle({ 'color': 'green' });
    }
    document.getElementById('closestLayer').innerHTML = closestLayer.layer.options.className;

    // display the closest position for snap
    document.getElementById('closestLayerSnap').innerHTML = closestLayerSnap ? closestLayerSnap.layer.options.className : 'unknown';

    // display the closest point on red segment
    markerClosestSegment = L.marker(closestOnSegment).addTo(_map).bindPopup('Closest point on segment');

})
