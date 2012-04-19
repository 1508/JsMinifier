# JsMinifier #
JsMinifier runs all .js files, though a httphandler, which uses Yahoo.Yui.Compressor to minifier and compress the file.

It has a exclude pattern, to exclude minified files, and other pathes specified in the configuration.

It also sets caching headers, to the client.

## Configuration ##

###Integrated mode###
Add the httphandler to system.webServer if you are using integrated mode

	<system.webServer>
		<handlers>
			<add name="JsMinifier" path="*.js" verb="GET" type="JsMinifier.Handler.HttpHandler, JsMinifier.Handler" resourceType="File" preCondition="" />
		</handlers>
	</system.webServer>


### Classic mode ###
	<system.web>
		<httpHandlers>
			<add path="*.js" verb="GET" type="JsMinifier.Handler.HttpHandler, JsMinifier.Handler" />
		</httpHandlers>
	</system.web

### JsMinifier module config ###
	<configSections>
		<section name="JsMinifier" type="JsMinifier.Handler.Configuration.JsMinifierConfiguration, JsMinifier.Handler"/>
	</configSections>

	<JsMinifier Cache="True" Minifiy="True">
		<Excludes>
			<Path>.m.</Path>
		</Excludes>
	</JsMinifier>

#### Cache ####
Sets if it should cache the transformed source in asp.net cache, and sets corresponding cache headers in the response to the client.

#### Minify ####
Sets if the file should be minifyed and compressed with yui compressor. 
Useful on developer environments

	