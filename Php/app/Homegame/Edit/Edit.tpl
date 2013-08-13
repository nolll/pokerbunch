{extends file="app/Page.tpl"}
{block name='title'}Edit Homegame{/block}
{block name=full}
	<div class="block gutter">
		<h1>{$model->heading}</h1>
	</div>
	<div class="block gutter">
		{partial view='app\Site\Errors' model=$model->validationErrors}

		<form method="post">
			<fieldset>
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
				<p>
					<label>Default buyin</label>
					<input type="text" name="defaultbuyin" value="{$model->defaultBuyin}" class="textfield"/>
				</p>
				<p>
					<label for="houserules">HouseRules</label>
					<textarea name="houserules" class="textfield">{$model->houseRules}</textarea>
				</p>
				{*
				<p class="checkbox-layout">
					<label class="checkbox-label" for="cashgames">Enable Cashgames</label>
					<input type="checkbox" name="cashgames" id="cashgames" value="1" {if $model->cashgamesEnabled}checked{/if} />
				</p>
				<p class="checkbox-layout">
					<label class="checkbox-label" for="tournaments">Enable Tournaments</label>
					<input type="checkbox" name="tournaments" id="tournaments" value="1" {if $model->tournamentsEnabled}checked{/if} />

				</p>
				<p class="checkbox-layout">
					<label class="checkbox-label" for="videos">Enable Videos</label>
					<input type="checkbox" name="videos" id="videos" value="1" {if $model->videosEnabled}checked{/if} />
				</p>
				*}
			</fieldset>
			<div class="buttons">
				<button type="submit" class="action">Save</button>
				<a href="{$model->cancelUrl->url}" class="button">Cancel</a>
			</div>
		</form>
	</div>
{/block}