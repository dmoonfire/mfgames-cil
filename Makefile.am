AUTOMAKE_OPTIONS=dist-bzip2 dist-zip
BUILD = $(top_builddir)/build
DOCBOOK_XSL = /usr/share/xml/docbook/stylesheet/nwalsh/xhtml/docbook.xsl
DOCBOOK_HTML_PARAMS = --stringparam html.stylesheet style.css

mfgames_utility_ASSEMBLY_NAME = MfGames.Utility
mfgames_utility_DLL = $(mfgames_utility_ASSEMBLY_NAME).dll
mfgames_utility_ASSEMBLY = $(BUILD)/$(mfgames_utility_DLL)
mfgames_utility_SOURCES =  \
src/AssemblyInfo.cs \
src/AttributeTree.cs \
src/AttributeTreeCollection.cs \
src/AuditMessageArgs.cs \
src/AuditMessageHandler.cs \
src/AuditSeverityArgs.cs \
src/AuditSeverityHandler.cs \
src/Auditable.cs \
src/NestedAuditable.cs \
src/ConfigStorage.cs \
src/ConsoleLogger.cs \
src/Entropy.cs \
src/IAuditable.cs \
src/IEntropy.cs \
src/ITickable.cs \
src/InvalidPathException.cs \
src/MersenneEntropy.cs \
src/MersenneTwister.cs \
src/MfConvert.cs \
src/MimeFilenameIdentifier.cs \
src/NodeRef.cs \
src/NotAbsolutePathException.cs \
src/SecondGuage.cs \
src/Severity.cs \
src/TickManager.cs \
src/TypeChooser.cs \
src/UtilityException.cs \
src/ValueTree.cs \
src/Version.cs \
src/WeightedSelector.cs \
src/asset/ListAssetProvider.cs \
src/asset/IAsset.cs \
src/asset/IAssetProvider.cs \
src/asset/AssemblyAsset.cs \
src/asset/AssemblyAssetProvider.cs \
src/asset/AssetException.cs \
src/dice/AdditionDice.cs \
src/dice/ConstantDice.cs \
src/dice/IDice.cs \
src/dice/Parser.cs \
src/dice/RandomDice.cs \
src/dice/Scanner.cs \
src/dice/SubtractionDice.cs \
src/dice/DiceException.cs \
src/dice/DiceFactory.cs \
src/log/ILogSink.cs \
src/log/Log.cs \
src/log/Logable.cs \
src/log/Logger.cs \
src/tool/LongArgumentAttribute.cs \
src/tool/CommandLineArguments.cs \
src/tool/ToolManager.cs \
src/tool/ITool.cs \
src/tool/ToolException.cs \
src/tool/UsageFormatter.cs \
src/tool/LongArgumentScanner.cs \
src/tool/ArgumentScanner.cs \
src/tool/IArgumentAttribute.cs \
src/tool/PositionalAttribute.cs \
src/tz/TimeZones.cs \
src/tz/TimeZoneUS.cs \
src/tz/TimeZoneUtc.cs \
src/math/Matrix4D.cs \
src/math/Vector2D.cs \
src/math/Vector3D.cs \
src/math/EulerRotation.cs \
src/math/Quaternion.cs

mfgames_log4net_ASSEMBLY_NAME = MfGames.Utility.Log4Net
mfgames_log4net_DLL = $(mfgames_log4net_ASSEMBLY_NAME).dll
mfgames_log4net_ASSEMBLY = $(BUILD)/$(mfgames_log4net_DLL)
mfgames_log4net_SOURCES = \
src/log4net/AssemblyInfo.cs \
src/log4net/Log4NetLogger.cs

test_DLL = test.dll
test_ASSEMBLY = $(BUILD)/$(test_DLL)
test_SOURCES = \
src/test/AttributeTreeTest.cs \
src/test/NodeRefTest.cs \
src/test/AssemblyAssetTest.cs \
src/test/UtilityTest.cs \
src/test/VersionTest.cs \
src/test/AuditTest.cs \
src/test/ToolTest.cs \
src/test/TestDice.cs \
src/test/TestTimeZones.cs

# Generic
if LOG4NET
all: $(mfgames_utility_ASSEMBLY) $(mfgames_log4net_ASSEMBLY) $(test_ASSEMBLY) mkdocs
else
all: $(mfgames_utility_ASSEMBLY) $(test_ASSEMBLY) mkdocs
endif

clean-local:
	$(RM) -rf $(BUILD)
	$(RM) -f *~ */*~ */*/*~

$(mfgames_utility_ASSEMBLY): $(mfgames_utility_SOURCES)
	mkdir -p $(BUILD)
	$(CSC) $(CSC_FLAGS) -target:library -out:$@ \
		-keyfile:etc/MfGames.Utility.snk \
		-doc:$(BUILD)/MfGames.Utility.xml \
		/d:TRACE /d:DEBUG \
		$(realpath $(mfgames_utility_SOURCES))

$(mfgames_log4net_ASSEMBLY): $(mfgames_log4net_SOURCES)
$(mfgames_log4net_ASSEMBLY): $(mfgames_utility_ASSEMBLY)
	$(CSC) $(CSC_FLAGS) -target:library -out:$@ \
		-keyfile:etc/MfGames.Utility.snk \
		-doc:$(BUILD)/MfGames.Utility.Log4net.xml \
		/r:$(mfgames_utility_ASSEMBLY) \
		@log4net_LIBS@ \
		$(realpath $(mfgames_log4net_SOURCES))

$(test_ASSEMBLY): $(test_SOURCES) $(mfgames_utility_ASSEMBLY)
	$(CSC) $(CSC_FLAGS) -target:library -out:$@ \
		@nunit_LIBS@ \
		-r:$(mfgames_utility_ASSEMBLY) \
		-resource:src/test/AssemblyAssetTest.cs \
		-resource:src/asset/AssemblyAssetProvider.cs,foo/bar \
		-resource:src/asset/AssemblyAsset.cs,/gary/bob \
		$(realpath $(test_SOURCES))

#	Delete the file if the nunit fails
	@NUNIT_CONSOLE@ /nologo $@ || rm $(test_ASSEMBLY)

#
# Dice
#

src/dice/Scanner.cs: src/dice/Parser.cs

src/dice/Parser.cs: src/dice/dice.atg
src/dice/Parser.cs: src/dice/Parser.frame
src/dice/Parser.cs: src/dice/Scanner.frame
	@COCOCS@ -namespace MfGames.Utility.Dice src/dice/dice.atg

#
# Documentation
#

mkdocs: $(BUILD)/index.html $(BUILD)/style.css

$(BUILD)/index.html: docs/docbook.xml
	mkdir -p $(BUILD)
	@XSLTPROC@ $(DOCBOOK_HTML_PARAMS) $(DOCBOOK_XSL) \
		docs/docbook.xml > $(BUILD)/index.html

$(BUILD)/style.css: docs/style.css
	mkdir -p $(BUILD)
	cp docs/style.css $(BUILD)/style.css

# pkgconfig
pkgconfig_in_files = etc/mfgames-utility-1.0.pc.in etc/mfgames-utility-log4net-1.0.pc.in
pkgconfigdir= $(prefix)/lib/pkgconfig
pkgconfig_DATA = $(pkgconfig_in_files:.pc.in=.pc)

# Installation
install-data-local:
	@GACUTIL@ /i $(mfgames_utility_ASSEMBLY) /f @GACUTIL_FLAGS@

# Uninstallation
uninstall-local:
	@GACUTIL@ /u $(mfgames_utility_ASSEMBLY_NAME) @GACUTIL_FLAGS@

# Definition of the various files that need to be cleaned up properly.
CLEANFILES = $(BUILD)/index.html $(BUILD)/style.css \
	$(BUILD)/MfGames.Utility.xml

EXTRA_DIST = $(pkgconfig_in_files) etc/MfGames.Utility.snk
EXTRA_DIST += src/dice/dice.atg src/dice/Parser.frame src/dice/Scanner.frame
EXTRA_DIST += $(mfgames_utility_SOURCES)
EXTRA_DIST += $(test_SOURCES)
EXTRA_DIST += docs/docbook.xml docs/style.css
EXTRA_DIST += vc7.1 vc7.1-nunit

DISTCLEANFILES = etc/mfgames-utility-1.0.pc etc/mfgames-utility-log4net-1.0.pc

# Debian
dist-debian:
	rm -rf tmp
	mkdir tmp
	-cp -ra * tmp 2> /dev/null
	find tmp -name .svn -o -name config.log -o -name config.status \
		| xargs rm -rf
	cd tmp && debuild
	mv *.dsc *.build *.changes *.deb *.tar.gz ..
	rm -rf tmp
