/*
 * Copyright (C) 2005, Moonfire Games
 *
 * This file is part of MfGames.Utility.
 *
 * The MfGames.Utility library is free software; you can redistribute
 * it and/or modify it under the terms of the GNU Lesser General
 * Public License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307
 * USA
 */

namespace MfGames.Utility
{
	using System.Collections;
	using System.IO;
	using System.Text.RegularExpressions;

	/// <summary>
	/// A static class that just tries to identify the mime type of a
	/// file based on its filename. This is just a simple lookup table,
	/// but handles a good number of the "basic" cases.
	/// </summary>
	public abstract class MimeFilenameIdentifier
	{
		// Contains all the types
		public static Hashtable types = new Hashtable();

		// Contains the logging scope
		private static readonly string LOG_SCOPE
		= "MfGames.Utility.Mime.MimeFilenameIdentifier";

		/// <summary>
		/// Identifies the type of file based on its filename/regex.
		/// </summary>
		public static string GetMimeType(string filename)
		{
			// Pull off the extension, remove the period
			string ext = Path.GetExtension(filename);
			ext = Regex.Replace(ext, "^\\.", "");
			string type = (string) types[ext];

			if (type != null)
			{
				Logger.Debug(LOG_SCOPE, "Trying {0} from {1}: {2}",
					ext, filename, type);
				return type;
			}

			// No clue, so use the offical default
			return "application/octet-stream";
		}

		// Contains the "default" loader
		static MimeFilenameIdentifier()
		{
			types["%"] = "application/x-trash";
			types["323"] = "text/h323";
			types["abw"] = "application/x-abiword";
			types["ai"] = "application/postscript";
			types["aif"] = "audio/x-aiff";
			types["aifc"] = "audio/x-aiff";
			types["aiff"] = "audio/x-aiff";
			types["alc"] = "chemical/x-alchemy";
			types["art"] = "image/x-jg";
			types["asc"] = "text/plain";
			types["asf"] = "video/x-ms-asf";
			types["asn"] = "chemical/x-ncbi-asn1";
			types["asn"] = "chemical/x-ncbi-asn1-spec";
			types["aso"] = "chemical/x-ncbi-asn1-binary";
			types["asx"] = "video/x-ms-asf";
			types["au"] = "audio/basic";
			types["avi"] = "video/x-msvideo";
			types["b"] = "chemical/x-molconn-Z";
			types["bak"] = "application/x-trash";
			types["bat"] = "application/x-msdos-program";
			types["bcpio"] = "application/x-bcpio";
			types["bib"] = "text/x-bibtex";
			types["bin"] = "application/octet-stream";
			types["bmp"] = "image/x-ms-bmp";
			types["book"] = "application/x-maker";
			types["bsd"] = "chemical/x-crossfire";
			types["c"] = "text/x-csrc";
			types["c++"] = "text/x-c++src";
			types["c3d"] = "chemical/x-chem3d";
			types["cac"] = "chemical/x-cache";
			types["cache"] = "chemical/x-cache";
			types["cascii"] = "chemical/x-cactvs-binary";
			types["cat"] = "application/vnd.ms-pki.seccat";
			types["cbin"] = "chemical/x-cactvs-binary";
			types["cc"] = "text/x-c++src";
			types["cdf"] = "application/x-cdf";
			types["cdr"] = "image/x-coreldraw";
			types["cdt"] = "image/x-coreldrawtemplate";
			types["cdx"] = "chemical/x-cdx";
			types["cdy"] = "application/vnd.cinderella";
			types["cef"] = "chemical/x-cxf";
			types["cer"] = "chemical/x-cerius";
			types["chm"] = "chemical/x-chemdraw";
			types["chrt"] = "application/x-kchart";
			types["cif"] = "chemical/x-cif";
			types["class"] = "application/x-java-vm";
			types["cls"] = "text/x-tex";
			types["cmdf"] = "chemical/x-cmdf";
			types["cml"] = "chemical/x-cml";
			types["cod"] = "application/vnd.rim.cod";
			types["com"] = "application/x-msdos-program";
			types["cpa"] = "chemical/x-compass";
			types["cpio"] = "application/x-cpio";
			types["cpp"] = "text/x-c++src";
			types["cpt"] = "application/mac-compactpro";
			types["cpt"] = "image/x-corelphotopaint";
			types["crl"] = "application/x-pkcs7-crl";
			types["crt"] = "application/x-x509-ca-cert";
			types["csf"] = "chemical/x-cache-csf";
			types["csh"] = "application/x-csh";
			types["csh"] = "text/x-csh";
			types["csm"] = "chemical/x-csml";
			types["csml"] = "chemical/x-csml";
			types["cs"] = "text/x-csharpsrc"; // made it up
			types["css"] = "text/css";
			types["csv"] = "text/comma-separated-values";
			types["ctab"] = "chemical/x-cactvs-binary";
			types["ctx"] = "chemical/x-ctx";
			types["cu"] = "application/cu-seeme";
			types["cub"] = "chemical/x-gaussian-cube";
			types["cxf"] = "chemical/x-cxf";
			types["cxx"] = "text/x-c++src";
			types["dat"] = "chemical/x-mopac-input";
			types["dcr"] = "application/x-director";
			types["deb"] = "application/x-debian-package";
			types["dif"] = "video/x-dv";
			types["diff"] = "text/plain";
			types["dir"] = "application/x-director";
			types["djv"] = "image/vnd.djvu";
			types["djvu"] = "image/vnd.djvu";
			types["dl"] = "video/dl";
			types["dll"] = "application/x-msdos-program";
			types["dmg"] = "application/x-apple-diskimage";
			types["dms"] = "application/x-dms";
			types["doc"] = "application/msword";
			types["dot"] = "application/msword";
			types["dv"] = "video/x-dv";
			types["dvi"] = "application/x-dvi";
			types["dx"] = "chemical/x-jcamp-dx";
			types["dxr"] = "application/x-director";
			types["emb"] = "chemical/x-embl-dl-nucleotide";
			types["embl"] = "chemical/x-embl-dl-nucleotide";
			types["ent"] = "chemical/x-ncbi-asn1-ascii";
			types["ent"] = "chemical/x-pdb";
			types["eps"] = "application/postscript";
			types["etx"] = "text/x-setext";
			types["exe"] = "application/x-msdos-program";
			types["ez"] = "application/andrew-inset";
			types["fb"] = "application/x-maker";
			types["fbdoc"] = "application/x-maker";
			types["fch"] = "chemical/x-gaussian-checkpoint";
			types["fchk"] = "chemical/x-gaussian-checkpoint";
			types["fig"] = "application/x-xfig";
			types["flac"] = "application/x-flac";
			types["fli"] = "video/fli";
			types["fm"] = "application/x-maker";
			types["frame"] = "application/x-maker";
			types["frm"] = "application/x-maker";
			types["gal"] = "chemical/x-gaussian-log";
			types["gam"] = "chemical/x-gamess-input";
			types["gamin"] = "chemical/x-gamess-input";
			types["gau"] = "chemical/x-gaussian-input";
			types["gcd"] = "text/x-pcs-gcd";
			types["gcf"] = "application/x-graphing-calculator";
			types["gcg"] = "chemical/x-gcg8-sequence";
			types["gen"] = "chemical/x-genbank";
			types["gf"] = "application/x-tex-gf";
			types["gif"] = "image/gif";
			types["gjc"] = "chemical/x-gaussian-input";
			types["gjf"] = "chemical/x-gaussian-input";
			types["gl"] = "video/gl";
			types["gnumeric"] = "application/x-gnumeric";
			types["gpt"] = "chemical/x-mopac-graph";
			types["gsf"] = "application/x-font";
			types["gsm"] = "audio/x-gsm";
			types["gtar"] = "application/x-gtar";
			types["h"] = "text/x-chdr";
			types["h++"] = "text/x-c++hdr";
			types["hdf"] = "application/x-hdf";
			types["hh"] = "text/x-c++hdr";
			types["hin"] = "chemical/x-hin";
			types["hpp"] = "text/x-c++hdr";
			types["hqx"] = "application/mac-binhex40";
			types["hs"] = "text/x-haskell";
			types["hta"] = "application/hta";
			types["htm"] = "text/html";
			types["html"] = "text/html";
			types["hxx"] = "text/x-c++hdr";
			types["ica"] = "application/x-ica";
			types["ice"] = "x-conference/x-cooltalk";
			types["ico"] = "image/x-icon";
			types["ics"] = "text/calendar";
			types["icz"] = "text/calendar";
			types["ief"] = "image/ief";
			types["iges"] = "model/iges";
			types["igs"] = "model/iges";
			types["iii"] = "application/x-iphone";
			types["inp"] = "chemical/x-gamess-input";
			types["ins"] = "application/x-internet-signup";
			types["iso"] = "application/x-iso9660-image";
			types["isp"] = "application/x-internet-signup";
			types["ist"] = "chemical/x-isostar";
			types["istr"] = "chemical/x-isostar";
			types["jad"] = "text/vnd.sun.j2me.app-descriptor";
			types["jar"] = "application/x-java-archive";
			types["java"] = "text/x-java";
			types["jdx"] = "chemical/x-jcamp-dx";
			types["jmz"] = "application/x-jmol";
			types["jng"] = "image/x-jng";
			types["jnlp"] = "application/x-java-jnlp-file";
			types["jpe"] = "image/jpeg";
			types["jpeg"] = "image/jpeg";
			types["jpg"] = "image/jpeg";
			types["js"] = "application/x-javascript";
			types["kar"] = "audio/midi";
			types["key"] = "application/pgp-keys";
			types["kil"] = "application/x-killustrator";
			types["kin"] = "chemical/x-kinemage";
			types["kpr"] = "application/x-kpresenter";
			types["kpt"] = "application/x-kpresenter";
			types["ksp"] = "application/x-kspread";
			types["kwd"] = "application/x-kword";
			types["kwt"] = "application/x-kword";
			types["latex"] = "application/x-latex";
			types["lha"] = "application/x-lha";
			types["lhs"] = "text/x-literate-haskell";
			types["lsf"] = "video/x-la-asf";
			types["lsx"] = "video/x-la-asf";
			types["ltx"] = "text/x-tex";
			types["lzh"] = "application/x-lzh";
			types["lzx"] = "application/x-lzx";
			types["m3u"] = "audio/mpegurl";
			types["m3u"] = "audio/x-mpegurl";
			types["m4a"] = "audio/mpeg";
			types["maker"] = "application/x-maker";
			types["man"] = "application/x-troff-man";
			types["mcif"] = "chemical/x-mmcif";
			types["mcm"] = "chemical/x-macmolecule";
			types["mdb"] = "application/msaccess";
			types["me"] = "application/x-troff-me";
			types["mesh"] = "model/mesh";
			types["mid"] = "audio/midi";
			types["midi"] = "audio/midi";
			types["mif"] = "application/x-mif";
			types["mm"] = "application/x-freemind";
			types["mmd"] = "chemical/x-macromodel-input";
			types["mmf"] = "application/vnd.smaf";
			types["mml"] = "text/mathml";
			types["mmod"] = "chemical/x-macromodel-input";
			types["mng"] = "video/x-mng";
			types["moc"] = "text/x-moc";
			types["mol"] = "chemical/x-mdl-molfile";
			types["mol2"] = "chemical/x-mol2";
			types["moo"] = "chemical/x-mopac-out";
			types["mop"] = "chemical/x-mopac-input";
			types["mopcrt"] = "chemical/x-mopac-input";
			types["mov"] = "video/quicktime";
			types["movie"] = "video/x-sgi-movie";
			types["mp2"] = "audio/mpeg";
			types["mp3"] = "audio/mpeg";
			types["mp4"] = "video/mp4";
			types["mpc"] = "chemical/x-mopac-input";
			types["mpe"] = "video/mpeg";
			types["mpeg"] = "video/mpeg";
			types["mpega"] = "audio/mpeg";
			types["mpg"] = "video/mpeg";
			types["mpga"] = "audio/mpeg";
			types["ms"] = "application/x-troff-ms";
			types["msh"] = "model/mesh";
			types["msi"] = "application/x-msi";
			types["mvb"] = "chemical/x-mopac-vib";
			types["mxu"] = "video/vnd.mpegurl";
			types["nb"] = "application/mathematica";
			types["nc"] = "application/x-netcdf";
			types["nwc"] = "application/x-nwc";
			types["o"] = "application/x-object";
			types["oda"] = "application/oda";
			types["odb"] = "application/vnd.oasis.opendocument.database";
			types["odc"] = "application/vnd.oasis.opendocument.chart";
			types["odf"] = "application/vnd.oasis.opendocument.formula";
			types["odg"] = "application/vnd.oasis.opendocument.graphics";
			types["odi"] = "application/vnd.oasis.opendocument.image";
			types["odm"] = "application/vnd.oasis.opendocument.text-master";
			types["odp"] = "application/vnd.oasis.opendocument.presentation";
			types["ods"] = "application/vnd.oasis.opendocument.spreadsheet";
			types["odt"] = "application/vnd.oasis.opendocument.text";
			types["ogg"] = "application/ogg";
			types["old"] = "application/x-trash";
			types["otg"] = "application/vnd.oasis.opendocument.graphics-template";
			types["oth"] = "application/vnd.oasis.opendocument.text-web";
			types["otp"] = "application/vnd.oasis.opendocument.presentation-template";
			types["ots"] = "application/vnd.oasis.opendocument.spreadsheet-template";
			types["ott"] = "application/vnd.oasis.opendocument.text-template";
			types["oza"] = "application/x-oz-application";
			types["p"] = "text/x-pascal";
			types["p7r"] = "application/x-pkcs7-certreqresp";
			types["pac"] = "application/x-ns-proxy-autoconfig";
			types["pas"] = "text/x-pascal";
			types["pat"] = "image/x-coreldrawpattern";
			types["pbm"] = "image/x-portable-bitmap";
			types["pcf"] = "application/x-font";
			types["pcf.Z"] = "application/x-font";
			types["pcx"] = "image/pcx";
			types["pdb"] = "chemical/x-pdb";
			types["pdf"] = "application/pdf";
			types["pfa"] = "application/x-font";
			types["pfb"] = "application/x-font";
			types["pgm"] = "image/x-portable-graymap";
			types["pgn"] = "application/x-chess-pgn";
			types["pgp"] = "application/pgp-signature";
			types["php"] = "application/x-httpd-php";
			types["php3"] = "application/x-httpd-php3";
			types["php3p"] = "application/x-httpd-php3-preprocessed";
			types["php4"] = "application/x-httpd-php4";
			types["phps"] = "application/x-httpd-php-source";
			types["pht"] = "application/x-httpd-php";
			types["phtml"] = "application/x-httpd-php";
			types["pk"] = "application/x-tex-pk";
			types["pl"] = "text/x-perl";
			types["pls"] = "audio/x-scpls";
			types["pm"] = "text/x-perl";
			types["png"] = "image/png";
			types["pnm"] = "image/x-portable-anymap";
			types["pot"] = "text/plain";
			types["ppm"] = "image/x-portable-pixmap";
			types["pps"] = "application/vnd.ms-powerpoint";
			types["ppt"] = "application/vnd.ms-powerpoint";
			types["prf"] = "application/pics-rules";
			types["prt"] = "chemical/x-ncbi-asn1-ascii";
			types["ps"] = "application/postscript";
			types["psd"] = "image/x-photoshop";
			types["py"] = "text/x-python";
			types["pyc"] = "application/x-python-code";
			types["pyo"] = "application/x-python-code";
			types["qt"] = "video/quicktime";
			types["qtl"] = "application/x-quicktimeplayer";
			types["ra"] = "audio/x-pn-realaudio";
			types["ra"] = "audio/x-realaudio";
			types["ram"] = "audio/x-pn-realaudio";
			types["rar"] = "application/rar";
			types["ras"] = "image/x-cmu-raster";
			types["rd"] = "chemical/x-mdl-rdfile";
			types["rdf"] = "application/rdf+xml";
			types["rgb"] = "image/x-rgb";
			types["rm"] = "audio/x-pn-realaudio";
			types["roff"] = "application/x-troff";
			types["ros"] = "chemical/x-rosdal";
			types["rpm"] = "application/x-redhat-package-manager";
			types["rss"] = "application/rss+xml";
			types["rtf"] = "text/rtf";
			types["rtx"] = "text/richtext";
			types["rxn"] = "chemical/x-mdl-rxnfile";
			types["sct"] = "text/scriptlet";
			types["sd"] = "chemical/x-mdl-sdfile";
			types["sd2"] = "audio/x-sd2";
			types["sda"] = "application/vnd.stardivision.draw";
			types["sdc"] = "application/vnd.stardivision.calc";
			types["sdd"] = "application/vnd.stardivision.impress";
			types["sdf"] = "chemical/x-mdl-sdfile";
			types["sdp"] = "application/vnd.stardivision.impress";
			types["sdw"] = "application/vnd.stardivision.writer";
			types["ser"] = "application/x-java-serialized-object";
			types["sgf"] = "application/x-go-sgf";
			types["sgl"] = "application/vnd.stardivision.writer-global";
			types["sh"] = "application/x-sh";
			types["sh"] = "text/x-sh";
			types["shar"] = "application/x-shar";
			types["shtml"] = "text/html";
			types["sid"] = "audio/prs.sid";
			types["sik"] = "application/x-trash";
			types["silo"] = "model/mesh";
			types["sis"] = "application/vnd.symbian.install";
			types["sit"] = "application/x-stuffit";
			types["skd"] = "application/x-koan";
			types["skm"] = "application/x-koan";
			types["skp"] = "application/x-koan";
			types["skt"] = "application/x-koan";
			types["smf"] = "application/vnd.stardivision.math";
			types["smi"] = "application/smil";
			types["smil"] = "application/smil";
			types["snd"] = "audio/basic";
			types["spc"] = "chemical/x-galactic-spc";
			types["spl"] = "application/futuresplash";
			types["spl"] = "application/x-futuresplash";
			types["src"] = "application/x-wais-source";
			types["stc"] = "application/vnd.sun.xml.calc.template";
			types["std"] = "application/vnd.sun.xml.draw.template";
			types["sti"] = "application/vnd.sun.xml.impress.template";
			types["stl"] = "application/vnd.ms-pki.stl";
			types["stw"] = "application/vnd.sun.xml.writer.template";
			types["sty"] = "text/x-tex";
			types["sv4cpio"] = "application/x-sv4cpio";
			types["sv4crc"] = "application/x-sv4crc";
			types["svg"] = "image/svg+xml";
			types["svgz"] = "image/svg+xml";
			types["sw"] = "chemical/x-swissprot";
			types["swf"] = "application/x-shockwave-flash";
			types["swfl"] = "application/x-shockwave-flash";
			types["sxc"] = "application/vnd.sun.xml.calc";
			types["sxd"] = "application/vnd.sun.xml.draw";
			types["sxg"] = "application/vnd.sun.xml.writer.global";
			types["sxi"] = "application/vnd.sun.xml.impress";
			types["sxm"] = "application/vnd.sun.xml.math";
			types["sxw"] = "application/vnd.sun.xml.writer";
			types["t"] = "application/x-troff";
			types["tar"] = "application/x-tar";
			types["taz"] = "application/x-gtar";
			types["tcl"] = "application/x-tcl";
			types["tcl"] = "text/x-tcl";
			types["tex"] = "text/x-tex";
			types["texi"] = "application/x-texinfo";
			types["texinfo"] = "application/x-texinfo";
			types["text"] = "text/plain";
			types["tgf"] = "chemical/x-mdl-tgf";
			types["tgz"] = "application/x-gtar";
			types["tif"] = "image/tiff";
			types["tiff"] = "image/tiff";
			types["tk"] = "text/x-tcl";
			types["tm"] = "text/texmacs";
			types["torrent"] = "application/x-bittorrent";
			types["tr"] = "application/x-troff";
			types["ts"] = "text/texmacs";
			types["tsp"] = "application/dsptype";
			types["tsv"] = "text/tab-separated-values";
			types["txt"] = "text/plain";
			types["uls"] = "text/iuls";
			types["ustar"] = "application/x-ustar";
			types["val"] = "chemical/x-ncbi-asn1-binary";
			types["vcd"] = "application/x-cdlink";
			types["vcf"] = "text/x-vcard";
			types["vcs"] = "text/x-vcalendar";
			types["vmd"] = "chemical/x-vmd";
			types["vms"] = "chemical/x-vamas-iso14976";
			types["vor"] = "application/vnd.stardivision.writer";
			types["vrm"] = "x-world/x-vrml";
			types["vrml"] = "model/vrml";
			types["vrml"] = "x-world/x-vrml";
			types["vsd"] = "application/vnd.visio";
			types["wad"] = "application/x-doom";
			types["wav"] = "audio/x-wav";
			types["wax"] = "audio/x-ms-wax";
			types["wbmp"] = "image/vnd.wap.wbmp";
			types["wbxml"] = "application/vnd.wap.wbxml";
			types["wk"] = "application/x-123";
			types["wm"] = "video/x-ms-wm";
			types["wma"] = "audio/x-ms-wma";
			types["wmd"] = "application/x-ms-wmd";
			types["wml"] = "text/vnd.wap.wml";
			types["wmlc"] = "application/vnd.wap.wmlc";
			types["wmls"] = "text/vnd.wap.wmlscript";
			types["wmlsc"] = "application/vnd.wap.wmlscriptc";
			types["wmv"] = "video/x-ms-wmv";
			types["wmx"] = "video/x-ms-wmx";
			types["wmz"] = "application/x-ms-wmz";
			types["wp5"] = "application/wordperfect5.1";
			types["wpd"] = "application/wordperfect";
			types["wrl"] = "model/vrml";
			types["wrl"] = "x-world/x-vrml";
			types["wsc"] = "text/scriptlet";
			types["wvx"] = "video/x-ms-wvx";
			types["wz"] = "application/x-wingz";
			types["xbm"] = "image/x-xbitmap";
			types["xcf"] = "application/x-xcf";
			types["xht"] = "application/xhtml+xml";
			types["xhtml"] = "application/xhtml+xml";
			types["xlb"] = "application/vnd.ms-excel";
			types["xls"] = "application/vnd.ms-excel";
			types["xlt"] = "application/vnd.ms-excel";
			types["xml"] = "application/xml";
			types["xpm"] = "image/x-xpixmap";
			types["xsl"] = "application/xml";
			types["xtel"] = "chemical/x-xtel";
			types["xul"] = "application/vnd.mozilla.xul+xml";
			types["xwd"] = "image/x-xwindowdump";
			types["xyz"] = "chemical/x-xyz";
			types["zip"] = "application/zip";
			types["zmt"] = "chemical/x-mopac-input";
			types["~"] = "application/x-trash";
		}
	}
}
