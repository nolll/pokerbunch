<tr>
    <td class="rank first">{$model->rank}.</td>
    <td class="name">
        <a href="{$model->playerUrl->url}">{$model->name}</a>
    </td>
    <td class="amount result {$model->resultClass}">{$model->totalResult}</td>
    {foreach item=cellModel from=$model->cellModels}
		{partial view='app\Cashgame\Matrix\Cell' model=$cellModel}
    {/foreach}
</tr>