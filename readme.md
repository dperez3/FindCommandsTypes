# Service Bus Commands Finder
The purpose of this tool is to assist in planning and estimating the migration of parts of Core Consumer functionality off of Core Consumers. It does so by categorizing Commands types and their usages throughout the organization and sorting them by number of dependents.


### Local Setup Requirement
1. Set [GitHub Username](https://github.com/dperez3/FindCommandsTypes/blob/master/DependenciesFinder/GitHubService.cs#L13)
1. Set path to Commands DLL
    - Clone [Commands Repo](https://github.com/im-customer-engagement/service-bus-commands)
    - `npm install && npm run build`
    - Set [`Defaults.CommandsDLLPath`](https://github.com/dperez3/FindCommandsTypes/blob/master/DependenciesFinder/Defaults.cs#L8-L9) to the local dll built by above^
    

### Run    
Both a Console and Web App project are available for displaying the Commands.