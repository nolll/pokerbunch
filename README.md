# pokerbunch

Frontend for pokerbunch.com

## Local development

- Clone backend project `https://github.com/nolll/pokerbunch-api.git`
- Make sure docker is running, and run `docker compose up` to setup the database
- Open backend solution in vs and hit F5
- Clone frontend project `https://github.com/nolll/pokerbunch.git`
- `npm install`
- `npm run serve`
- Head to `https://localhost:9001`
- Login with a user: `admin`, `manager` or `player`. The password is `abcd`

Some actions sends emails. For that to work, install smtp4dev
`dotnet tool install -g Rnwood.Smtp4dev` and run it with `smtp4dev`
