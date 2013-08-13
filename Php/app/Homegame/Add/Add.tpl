{extends file="app/Page.tpl"}
{block name='title'}Create Homegame{/block}
{block name=full}
	<div class="block gutter">
		<h1>Create Homegame</h1>
	</div>
	<div class="block gutter">
		{partial view='app\Site\Errors' model=$model->validationErrors}

		<form method="post">
			<fieldset>
				<p>
					<label>Display Name</label>
					<input type="text" name="displayname" value="{$model->displayName}" class="textfield"/>
				</p>
				<p>
					<label for="description">Description</label>
					<textarea name="description" class="textfield">{$model->description}</textarea>
				</p>
				<p>
					<label>Currency Symbol</label>
					<input type="text" name="currencysymbol" id="currencysymbol" value="{$model->currencySymbol}" class="textfield" data-require="currency-form"/>
				</p>
				<p>
					<label>Currency Layout</label>
					{partial view='core\FormFields\SelectField' model=$model->currencyLayoutSelectModel}
				</p>
				<p>
					<label>Timezone</label>
					{partial view='core\FormFields\SelectField' model=$model->timezoneSelectModel}
				</p>
			</fieldset>
			<div class="buttons">
				<button class="action" type="submit">Save</button>
			</div>
		</form>
	</div>
{/block}