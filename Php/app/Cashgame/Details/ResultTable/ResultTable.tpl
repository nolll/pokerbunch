{if !empty($model->resultModels)}
    <h2>Results</h2>

    <table class="cashgames">
        <thead>
        <tr>
            <th>Player</th>
            <th>Buyin</th>
            <th>Cashout</th>
            <th>Result</th>
            <th>Winrate</th>
        </tr>
        </thead>
        <tbody>
            {foreach $model->resultModels as $resultModel}
				{partial view='app\Cashgame\Details\ResultTableItem\ResultTableItem' model=$resultModel}
            {/foreach}
        </tbody>
    </table>
{/if}