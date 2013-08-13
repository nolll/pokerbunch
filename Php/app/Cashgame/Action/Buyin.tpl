{extends file="app/Page.tpl"}
{block name='title'}Buy In{/block}
{block name=full}
	<form method="post" action="{$model->buyinUrl->url}">
		<fieldset>
			<div class="block gutter">
				<h2>Buy In</h2>
				{partial view='app\Site\Errors' model=$model->validationErrors}
				<div>
					amount:
					<input type="number" name="amount" id="amount" value="{$model->buyinAmount}" class="numberfield" pattern="[0-9]*" required min="0" data-require="focus-text-selector"/>
				</div>
				<div>
					{if $model->stackFieldEnabled}
						stack size before buyin:
						<input type="number" name="stack" id="stack" value="0" class="numberfield" pattern="[0-9]*" required min="0" data-require="focus-text-selector"/>
					{/if}
				</div>
				<div class="buttons">
					<button type="submit" class="action">Buy In</button>
				</div>
			</div>
		</fieldset>
	</form>
{/block}