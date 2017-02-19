﻿// ***********************************************************************
// Assembly         : ACBr.Net.Consulta
// Author           : RFTD
// Created          : 02-16-2017
//
// Last Modified By : RFTD
// Last Modified On : 02-16-2017
// ***********************************************************************
// <copyright file="ACBrConsultaBase.cs" company="ACBr.Net">
//		        		   The MIT License (MIT)
//	     		    Copyright (c) 2014 - 2017 Grupo ACBr.Net
//
//	 Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//	 The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//	 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.IO;
using System.Net;
using System.Text;
using ACBr.Net.Core;
using ACBr.Net.Core.Exceptions;
using ACBr.Net.Core.Extensions;

namespace ACBr.Net.Consulta
{
	public abstract class ACBrConsultaBase : ACBrComponent
	{
		#region Field

		private CookieContainer cookies;

		#endregion Field

		#region Method

		#region Protected Method

		protected virtual HttpWebRequest GetClient(string url)
		{
			var webRequest = (HttpWebRequest)WebRequest.Create(url);
			webRequest.CookieContainer = cookies;
			webRequest.ProtocolVersion = HttpVersion.Version10;
			webRequest.UserAgent = "Mozilla/4.0 (compatible; Synapse)";

			webRequest.KeepAlive = true;
			webRequest.Headers.Add(HttpRequestHeader.KeepAlive, "300");

			return webRequest;
		}

		protected virtual string GetHtmlResponse(WebResponse response)
		{
			return GetHtmlResponse(response, Encoding.GetEncoding("ISO-8859-1"));
		}

		protected virtual string GetHtmlResponse(WebResponse response, Encoding enconder)
		{
			Guard.Against<ACBrException>(response.IsNull(), "Erro ao acessar o site.");

			string retorno;

			// ReSharper disable once AssignNullToNotNullAttribute
			using (var stHtml = new StreamReader(response.GetResponseStream(), enconder))
				retorno = stHtml.ReadToEnd();

			return retorno;
		}

		protected override void OnInitialize()
		{
			cookies = new CookieContainer();
		}

		protected override void OnDisposing()
		{
		}

		#endregion Protected Method

		#endregion Method
	}
}