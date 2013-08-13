<dl>
    <dt>Rank</dt>
    <dd class="rank first">{$model->rank}.</dd>

    <dt>Player</dt>
    <dd class="name">
        <a href="{$model->playerUrl->url}">{$model->name}</a>
    </dd>

    <dt>Winnings</dt>
    <dd class="amount result {$model->resultClass}">{$model->totalResult}</dd>

    <dt>Time played</dt>
    <dd class="gametime">{$model->gameTime}</dd>

    <dt>Winrate</dt>
    <dd class="winrate">{$model->winRate}</dd>
</dl>