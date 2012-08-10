#ifndef EU3PROVINCE_H_
#define EU3PROVINCE_H_


#include <string>
#include <vector>
#include <map>
#include "..\Date.h"
#include "..\Mappers.h"
using namespace std;



class Object;
class CK2Barony;
class CK2Province;
class EU3History;
class EU3Ruler;
class EU3Advisor;

class EU3Province
{
	public:
		EU3Province(int _num, Object* obj, date startDate, map< string, vector<string> >& mapSpreadStrings);
		
		void		output(FILE*);

		void		convert(int _num, bool _inHRE, vector<string> _discoveredBy);
		void		addAdvisor(EU3Advisor*);
		void		determineCulture(const cultureMapping& cultureMap, const vector<CK2Province*>& srcProvinces, const vector<CK2Barony*> baronies);
		void		determineReligion(const religionMapping& religionMap, const vector<CK2Province*>& srcProvinces);

		void		addCore(string core)			{ cores.push_back(core); };
		void		setOwner(string _owner)		{ owner = _owner; };

		string	getOwner()	const { return owner; };
		
	private:
		string determineEU3Culture(const cultureMapping& cultureMap, const CK2Province* srcProvince);

		int							num;
		string						capital;
		string						owner;
		vector<string>				cores;
		bool							inHRE;
		vector<string>				discoveredBy;
		string						culture;
		string						religion;
		vector<EU3History*>		history;
		vector<string>				modifiers;
};



#endif	// EU3PROVINCE_H_