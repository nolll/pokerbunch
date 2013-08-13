<td{if $model->hasBestResult} class="winner"{/if}>
    {if $model->showResult}
        <span class="amount {$model->resultClass}">{$model->winnings}</span>
        {if $model->showTransactions}
            <span class="transaction amount">in {$model->buyin}</span>
            <span class="transaction amount">out {$model->cashout}</span>
        {/if}
    {/if}
</td>