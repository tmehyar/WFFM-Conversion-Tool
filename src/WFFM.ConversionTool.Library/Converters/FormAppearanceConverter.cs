﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFFM.ConversionTool.Library.Constants;
using WFFM.ConversionTool.Library.Factories;
using WFFM.ConversionTool.Library.Helpers;
using WFFM.ConversionTool.Library.Models.Metadata;
using WFFM.ConversionTool.Library.Models.Sitecore;
using WFFM.ConversionTool.Library.Processors;
using WFFM.ConversionTool.Library.Providers;
using WFFM.ConversionTool.Library.Repositories;

namespace WFFM.ConversionTool.Library.Converters
{
	public class FormAppearanceConverter : ItemProcessor
	{
		private IDestMasterRepository _destMasterRepository;
		private IMetadataProvider _metadataProvider;
		private IFieldProvider _fieldProvider;
		private AppSettings _appSettings;

		public FormAppearanceConverter(IMetadataProvider metadataProvider, IDestMasterRepository destMasterRepository, IItemConverter itemConverter, IItemFactory itemFactory, IFieldProvider fieldProvider, AppSettings appSettings)
			: base(destMasterRepository, itemConverter, itemFactory, appSettings)
		{
			_destMasterRepository = destMasterRepository;
			_metadataProvider = metadataProvider;
			_fieldProvider = fieldProvider;
			_appSettings = appSettings;
		}

		public void ConvertTitle(SCItem form, SCItem pageItem)
		{
			var titleItemName = "Title";
			var textMetadata = _metadataProvider.GetItemMetadataByTemplateName("Text");

			pageItem = CheckItemNotNullForAnalysis(pageItem);

			DeleteItem(pageItem.ID, titleItemName, textMetadata);

			var showTitle = form.Fields.FirstOrDefault(field => field.FieldId == new Guid(FormConstants.FormShowTitleFieldId));
			if (showTitle == null || showTitle.Value == "1")
			{
				// Create Text Item with text in Title field using Title Tag HTML element
				var title = form.Fields.FirstOrDefault(field => field.FieldId == new Guid(FormConstants.FormTitleFieldId))?.Value;
				if (!string.IsNullOrEmpty(title))
				{
					var titleTagField = form.Fields.FirstOrDefault(field => field.FieldId == new Guid(FormConstants.FormTitleTagFieldId));
					var titleTag = titleTagField != null ? titleTagField.Value : FormConstants.FormTitleTagStandardValue;

					// Create Text item
					var parentItem = _destMasterRepository.GetSitecoreItem(pageItem.ID);
					var fieldValues = _fieldProvider.GetFieldValues(form, new Guid(FormConstants.FormTitleFieldId), titleItemName);

					// Set text field
					textMetadata.fields.newFields.First(field => field.destFieldId == new Guid(TextConstants.TextFieldId)).values = fieldValues;
                    // Set Html Tag field
                    textMetadata.fields.newFields.First(field => field.destFieldId == new Guid(TextConstants.TextHtmlTagFieldId)).value =
                        ConvertTitleTag(titleTag);
                    // Set __Sortorder field
                    textMetadata.fields.newFields.First(field => field.destFieldId == new Guid(BaseTemplateConstants.SortOrderFieldId)).value = "-100"; // First item in the page

					WriteNewItem(textMetadata.destTemplateId, parentItem, titleItemName, textMetadata);
				}
			}
		}

		public void ConvertIntroduction(SCItem form, SCItem pageItem)
		{
			var introductionItemName = "Introduction";
			var textMetadata = _metadataProvider.GetItemMetadataByTemplateName("RawHtml");

			pageItem = CheckItemNotNullForAnalysis(pageItem);

			DeleteItem(pageItem.ID, introductionItemName, textMetadata);

			var showIntroduction = form.Fields.FirstOrDefault(field => field.FieldId == new Guid(FormConstants.FormShowIntroductionFieldId));
			if (showIntroduction != null && showIntroduction.Value == "1")
			{
				// Create Text Item with text in Introduction field
				var introduction = form.Fields.FirstOrDefault(field => field.FieldId == new Guid(FormConstants.FormIntroductionFieldId))?.Value;
				if (!string.IsNullOrEmpty(introduction))
				{
					// Create Text item
					var parentItem = _destMasterRepository.GetSitecoreItem(pageItem.ID);					
					var fieldValues = _fieldProvider.GetFieldValues(form, new Guid(FormConstants.FormIntroductionFieldId), string.Empty, false);

					// Set text field
					textMetadata.fields.newFields.First(field => field.destFieldId == new Guid(SFERawTextConstants.HtmlFieldId)).values = fieldValues;
					// Set __Sortorder field
					textMetadata.fields.newFields.First(field => field.destFieldId == new Guid(BaseTemplateConstants.SortOrderFieldId)).value = "-50"; // Second item in the page, after title

					WriteNewItem(textMetadata.destTemplateId, parentItem, introductionItemName, textMetadata);
				}
			}
		}

		public void ConvertFooter(SCItem form, SCItem pageItem)
		{
			var footerItemName = "Footer";
			var textMetadata = _metadataProvider.GetItemMetadataByTemplateName("RawHtml");

			pageItem = CheckItemNotNullForAnalysis(pageItem);

			DeleteItem(pageItem.ID, footerItemName, textMetadata);

			var showFooter = form.Fields.FirstOrDefault(field => field.FieldId == new Guid(FormConstants.FormShowFooterFieldId));
			if (showFooter != null && showFooter.Value == "1")
			{
				// Create Text Item with text in Introduction field
				var footer = form.Fields.FirstOrDefault(field => field.FieldId == new Guid(FormConstants.FormFooterFieldId))?.Value;
				if (!string.IsNullOrEmpty(footer))
				{
					// Create Text item
					var parentItem = _destMasterRepository.GetSitecoreItem(pageItem.ID);
					var fieldValues = _fieldProvider.GetFieldValues(form, new Guid(FormConstants.FormFooterFieldId), string.Empty, false);

					// Set text field
					textMetadata.fields.newFields.First(field => field.destFieldId == new Guid(SFERawTextConstants.HtmlFieldId)).values = fieldValues;
					// Set __Sortorder field
					textMetadata.fields.newFields.First(field => field.destFieldId == new Guid(BaseTemplateConstants.SortOrderFieldId)).value = "4000"; // Second item in the page, after title

					WriteNewItem(textMetadata.destTemplateId, parentItem, footerItemName, textMetadata);
				}
			}
		}

		private string ConvertTitleTag(string sourceTitleTagValue)
		{
			var tagValue = sourceTitleTagValue.ToLower();
			switch (tagValue)
			{
				case "a":
				case "b":
				case "strong":
					return "h1";
				default:
					return tagValue;
			}
		}
	}
}