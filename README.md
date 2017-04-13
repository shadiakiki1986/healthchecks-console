# healthchecks-console
Console application that wraps the REST API of [healthchecks](https://github.com/healthchecks/healthchecks), which is also hosted at https://healthchecks.io

- Written in golang
- As of 2017-04-12, this is still WIP
- Downloadable compiled binaries in the releases page

## Usage

Configure

- `hc configure:api-key <api key>`
- `hc configure:endpoint https://healthchecks.io`
- stores configurations in file `~/.healthchecksrc`

Update local cache of list of checks

- `hc update`
- caches the list of checks in a file `~/.cache/healthchecks/list.json`

Ping a check by name

- `hc ping "name of check"
- `hc ping --tags foo,bar "name of check"
- `--tags`: coma-separated list of tags
- the combination of the name of the check with the tags should resolve to a single check, otherwise the program exits with a non-zero exit code

Syncronize a cron file with healthchecks

- `hc sync /etc/cron.d/filename`
- syncronizes only lines in the cron file that contain `&& hc ping ...`
- all the "..." in `hc ping ...` should resolve to either 0 or 1 checks, otherwise the syncronization does not happen
  - if it resolves to 0 checks, a new check is created, with the period being that of the cron job
  - if it resolves to 1 check, the check's period is updated to match that of the cron job
- a file `~/.cache/healthchecks/sync.json` will contain a mapping of cron files synced with their synced cron jobs.
  - This is used to identify deleted checks
  - The check in this case is de-activated on the healthchecks endpoint
  - Use the `prune` command to delete de-activated checks

Prune inactive checks

- `hc prune`
- Prompts the user for each check that would be deleted
- inactive checks come from checks that were in a cron file and that were later removed

## License
Check [[LICENSE]]
