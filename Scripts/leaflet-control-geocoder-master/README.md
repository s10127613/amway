## A few words on diversity in tech

I need to take some of your time. I can't believe we let shit like [the Kathy Sierra incident](http://www.wired.com/2014/10/trolls-will-always-win/) or [what happened to Brianna Wu](https://twitter.com/Spacekatgal/status/520739878993420290) happen over and over again. I can't believe we, the open source community, let [sexist, misogynous shit happen over and over again](http://geekfeminism.wikia.com/wiki/Timeline_of_incidents).

I strongly believe that it is my &mdash; and your &mdash; duty to make the open source community, as well as the tech community at large, a community where everyone feel welcome and is accepted. At the very minimum, that means making sure the community and its forums both _are_ safe, and are perceived as safe. It means being friendly and inclusive, even when you disagree with people. It means not shrugging off discussions about sexism and inclusiveness with [handwaving about censorship and free speech](https://josm.openstreetmap.de/ticket/10568). For a more elaborate document on what that means, [the NPM Code of Conduct](http://www.npmjs.com/policies/conduct) is a good start, [Geek Feminism's resources for allies](http://geekfeminism.wikia.com/wiki/Resources_for_allies) contains much more.

While I can't force anyone to do anything, if you happen to disagree with this, I ask of you not to use any of the open source I have published. Nor am I interested in contributions from people who can't accept or act respectfully towards other humans regardless of gender identity, sexual orientation, disability, ethnicity, religion, age, physical appearance, body size, race, or similar personal characteristics. If you think feminism, anti-racism or the LGBT movement is somehow wrong, disturbing or irrelevant, I ask you to go elsewhere to find software.

# Leaflet Control Geocoder [![NPM version](https://img.shields.io/npm/v/leaflet-control-geocoder.svg)](https://www.npmjs.com/package/leaflet-control-geocoder) ![Leaflet 1.0.0 compatible!](https://img.shields.io/badge/Leaflet%201.0.0-%E2%9C%93-1EB300.svg?style=flat)

A simple geocoder for [Leaflet](https://leafletjs.com/) that by default uses [OSM](https://www.openstreetmap.org/)/[Nominatim](https://wiki.openstreetmap.org/wiki/Nominatim).

The plugin supports many different data providers:

- LatLng to parse basic latitude/longitude strings
- [OSM](https://www.openstreetmap.org/)/[Nominatim](https://wiki.openstreetmap.org/wiki/Nominatim)
- [Bing Locations API](http://msdn.microsoft.com/en-us/library/ff701715.aspx)
- [Google Geocoding API](https://developers.google.com/maps/documentation/geocoding/)
- [Mapbox Geocoding](https://www.mapbox.com/api-documentation/#geocoding)
- [MapQuest Geocoding API](http://developer.mapquest.com/web/products/dev-services/geocoding-ws)
- [What3Words](http://what3words.com/)
- [Photon](http://photon.komoot.de/)
- [Pelias](https://pelias.io/), [geocode.earth](https://geocode.earth/) (formerly Mapzen Search), [Openrouteservice](https://openrouteservice.org/dev/#/api-docs/geocode)
- [HERE Geocoder API](https://developer.here.com/documentation/geocoder/topics/introduction.html)
- [Neutrino API](https://www.neutrinoapi.com/api/geocode-address/)
- [Plus codes](https://plus.codes/) (formerly OpenLocationCode) (requires [open-location-code](https://www.npmjs.com/package/open-location-code))

The plugin can easily be extended to support other providers. Current extensions:

- [DAWA Geocoder](https://github.com/kjoller/leaflet-control-geocoder-dawa/tree/new) - support for Danish Address Web API by [Niels Kj??ller Hansen](https://github.com/kjoller)

# Demos

- [Leaflet Control Geocoder Demo](https://www.liedman.net/leaflet-control-geocoder/) hosted on liedman.net
- See [demo/](https://github.com/perliedman/leaflet-control-geocoder/tree/master/demo)
- See [demo-rollup/](https://github.com/perliedman/leaflet-control-geocoder/tree/master/demo-rollup) using the [rollup.js](https://rollupjs.org/) bundler
- See [demo-webpack/](https://github.com/perliedman/leaflet-control-geocoder/tree/master/demo-rollup) using the [webpack](https://webpack.js.org/) bundler

# Usage

[Download latest release](https://github.com/perliedman/leaflet-control-geocoder/releases), or obtain the latest release via [unpkg.com](https://unpkg.com/):

```HTML
<link rel="stylesheet" href="https://unpkg.com/leaflet-control-geocoder/dist/Control.Geocoder.css" />
<script src="https://unpkg.com/leaflet-control-geocoder/dist/Control.Geocoder.js"></script>
```

Add the control to a map instance:

```javascript
var map = L.map('map').setView([0, 0], 2);
L.tileLayer('https://{s}.tile.osm.org/{z}/{x}/{y}.png', {
  attribution: '&copy; <a href="https://osm.org/copyright">OpenStreetMap</a> contributors'
}).addTo(map);
L.Control.geocoder().addTo(map);
```

# Customizing

By default, when a geocoding result is found, the control will center the map on it and place
a marker at its location. This can be customized by listening to the control's `markgeocode`
event. To remove the control's default handler for marking a result, set the option
`defaultMarkGeocode` to `false`.

For example:

```javascript
var geocoder = L.Control.geocoder({
  defaultMarkGeocode: false
})
  .on('markgeocode', function(e) {
    var bbox = e.geocode.bbox;
    var poly = L.polygon([
      bbox.getSouthEast(),
      bbox.getNorthEast(),
      bbox.getNorthWest(),
      bbox.getSouthWest()
    ]).addTo(map);
    map.fitBounds(poly.getBounds());
  })
  .addTo(map);
```

This will add a polygon representing the result's boundingbox when a result is selected.

# API

## L.Control.Geocoder

This is the geocoder control. It works like any other Leaflet control, and is added to the map.

### Constructor

This plugin supports the standard JavaScript constructor (to be invoked using `new`) as well as the [class factory methods](https://leafletjs.com/reference.html#class-class-factories) known from Leaflet:

```js
new L.Control.Geocoder(options);
// or
L.Control.geocoder(options);
```

### Options

| Option             | Type      | Default                              | Description                                                                                                   |
| ------------------ | --------- | ------------------------------------ | ------------------------------------------------------------------------------------------------------------- |
| `collapsed`        | Boolean   | `true`                               | Collapse control unless hovered/clicked                                                                       |
| `expand`           | String    | `"touch"`                            | How to expand a collapsed control: `touch` or `click` or `hover`                                              |
| `position`         | String    | `"topright"`                         | Control [position](https://leafletjs.com/reference.html#control-positions)                                    |
| `placeholder`      | String    | `"Search..."`                        | Placeholder text for text input                                                                               |
| `errorMessage`     | String    | `"Nothing found."`                   | Message when no result found / geocoding error occurs                                                         |
| `iconLabel`        | String    | `"Initiate a new search"`            | Accessibility label for the search icon used by screen readers                                                |
| `geocoder`         | IGeocoder | `new L.Control.Geocoder.Nominatim()` | Object to perform the actual geocoding queries                                                                |
| `showUniqueResult` | Boolean   | `true`                               | Immediately show the unique result without prompting for alternatives                                         |
| `showResultIcons`  | Boolean   | `false`                              | Show icons for geocoding results (if available); supported by Nominatim                                       |
| `suggestMinLength` | Number    | `3`                                  | Minimum number characters before suggest functionality is used (if available from geocoder)                   |
| `suggestTimeout`   | Number    | `250`                                | Number of milliseconds after typing stopped before suggest functionality is used (if available from geocoder) |
| `query`            | String    | `""`                                 | Initial query string for text input                                                                           |
| `queryMinLength`   | Number    | `1`                                  | Minimum number of characters in search text before performing a query                                         |

### Methods

| Method                                  | Returns | Description                             |
| --------------------------------------- | ------- | --------------------------------------- |
| `markGeocode(<GeocodingResult> result)` | `this`  | Marks a geocoding result on the map     |
| `setQuery(<String> query)`              | `this`  | Sets the query string on the text input |

## L.Control.Geocoder.Nominatim

Uses [Nominatim](https://wiki.openstreetmap.org/wiki/Nominatim) to respond to geocoding queries. This is the default
geocoding service used by the control, unless otherwise specified in the options. Implements `IGeocoder`.

Unless using your own Nominatim installation, please refer to the [Nominatim usage policy](https://operations.osmfoundation.org/policies/nominatim/).

### Constructor

```js
new L.Control.Geocoder.Nominatim(options);
// or
L.Control.Geocoder.nominatim(options);
```

### Options

| Option                 | Type     | Default                                  | Description                                                                                                                                                                                                                                                         |
| ---------------------- | -------- | ---------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `serviceUrl`           | String   | `"https://nominatim.openstreetmap.org/"` | URL of the service                                                                                                                                                                                                                                                  |
| `geocodingQueryParams` | Object   | `{}`                                     | Additional URL parameters (strings) that will be added to geocoding requests; can be used to restrict results to a specific country for example, by providing the [`countrycodes`](https://wiki.openstreetmap.org/wiki/Nominatim#Parameters) parameter to Nominatim |
| `reverseQueryParams`   | Object   | `{}`                                     | Additional URL parameters (strings) that will be added to reverse geocoding requests                                                                                                                                                                                |
| `htmlTemplate`         | function | special                                  | A function that takes an GeocodingResult as argument and returns an HTML formatted string that represents the result. Default function breaks up address in parts from most to least specific, in attempt to increase readability compared to Nominatim's naming    |

## L.Control.Geocoder.Bing

Uses [Bing Locations API](http://msdn.microsoft.com/en-us/library/ff701715.aspx) to respond to geocoding queries. Implements `IGeocoder`.

Note that you need an API key to use this service.

### Constructor

```ts
new L.Control.Geocoder.Bing(<String>key);
// or
L.Control.Geocoder.bing(<String>key);
```

## L.Control.Geocoder.OpenCage

Uses [OpenCage Data API](https://opencagedata.com/) to respond to geocoding queries. Implements `IGeocoder`.

Note that you need an API key to use this service.

### Constructor

```ts
new L.Control.Geocoder.OpenCage(<String>key, options);
// or
L.Control.Geocoder.opencage(<String>key, options);
```

### Options

| Option                 | Type   | Default                                          | Description                                                                          |
| ---------------------- | ------ | ------------------------------------------------ | ------------------------------------------------------------------------------------ |
| `serviceUrl`           | String | `"https://api.opencagedata.com/geocode/v1/json"` | URL of the service                                                                   |
| `geocodingQueryParams` | Object | `{}`                                             | Additional URL parameters (strings) that will be added to geocoding requests         |
| `reverseQueryParams`   | Object | `{}`                                             | Additional URL parameters (strings) that will be added to reverse geocoding requests |

## L.Control.Geocoder.LatLng

Parses basic latitude/longitude strings such as `'50.06773 14.37742'`, `'N50.06773 W14.37742'`, `'S 50?? 04.064 E 014?? 22.645'`, or `'S 50?? 4??? 03.828???, W 14?? 22??? 38.712???'`.

### Constructor

```ts
new L.Control.Geocoder.LatLng(options);
// or
L.Control.Geocoder.latLng(options);
```

### Options

| Option         | Type      | Default | Description                                               |
| -------------- | --------- | ------- | --------------------------------------------------------- |
| `next`         | IGeocoder |         | The next geocoder to use for non-supported queries.       |
| `sizeInMeters` | Number    | 10000   | The size in meters used for passing to `LatLng.toBounds`. |

## IGeocoder

An interface implemented to respond to geocoding queries.

### Methods

| Method                                                            | Returns             | Description                                                                                                                       |
| ----------------------------------------------------------------- | ------------------- | --------------------------------------------------------------------------------------------------------------------------------- |
| `geocode(<String> query, callback, context)`                      | `GeocodingResult[]` | Performs a geocoding query and returns the results to the callback in the provided context                                        |
| `suggest(<String> query, callback, context)`                      | `GeocodingResult[]` | Performs a geocoding query suggestion (this happens while typing) and returns the results to the callback in the provided context |
| `reverse(<L.LatLng> location, <Number> scale, callback, context)` | `GeocodingResult[]` | Performs a reverse geocoding query and returns the results to the callback in the provided context                                |

## GeocodingResult

An object that represents a result from a geocoding query.

### Properties

| Property | Type           | Description                                          |
| -------- | -------------- | ---------------------------------------------------- |
| `name`   | String         | Name of found location                               |
| `bbox`   | L.LatLngBounds | The bounds of the location                           |
| `center` | L.LatLng       | The center coordinate of the location                |
| `icon`   | String         | URL for icon representing result; optional           |
| `html`   | String         | (optional) HTML formatted representation of the name |
