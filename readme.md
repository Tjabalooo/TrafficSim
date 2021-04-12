# Traffic Simulator (TrafficSim)
A HttpClient making a call each second. What call is made is based on the setup supplied to the TrafficSim through a json config.

## Purpose
When implementing software productivity metrics you can either go on a "click frenzy" or you can set up some kind of simulation that helps you out. The TrafficSim is a small and simple tool made for this purpose.

## The config
The config is a json object depicting a set of named states, where these states can go and how the different steps are weighed. In the example below the TrafficSim will start out at google.com and then 2 out of 3 times it will choose to search for 'github'. After a search is done it will always go back to google.com.
```
{
    "states": [
		{
			"name": "google",
			"path": "https://www.google.com",
			"steps": [
				{
					"action": "refresh",
					"to": "google",
					"weight": 1
				},
				{
					"action": "search for github",
					"to": "google github",
					"weight": 2
				}
			]
		},
		{
			"name": "google github",
			"path": "https://www.google.se/search?q=github",
			"steps": [
				{
					"action": "go back to clean search window",
					"to": "google",
					"weight": 1
				}
			]
		}
    ]
}